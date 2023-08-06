using System;
using System.Collections.Generic;
using System.Linq;
using CongestionTaxCalculator.Application.Interfaces;
using CongestionTaxCalculator.Application.Interfaces.Repositories;
using CongestionTaxCalculator.Application.Interfaces.Services;
using CongestionTaxCalculator.Application.Resources;
using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Application
{
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        private readonly ITaxRuleService _taxRuleService;
        private readonly IVehiclePassRepository _vehiclePassRepository;

        public CongestionTaxCalculator(ITaxRuleService taxRuleService, IVehiclePassRepository vehiclePassRepository)
        {
            _taxRuleService = taxRuleService;
            _vehiclePassRepository = vehiclePassRepository;
        }

        /// <summary>
        ///     Calculate the total toll fee
        /// </summary>
        /// <param name="licensePlate">The vehicle's license plate</param>
        /// <returns>The total congestion tax</returns>
        public int GetTax(string licensePlate)
        {
            var maxAmount = int.Parse(Constants.MaxAmountPerDayAndVehicle);

            return _vehiclePassRepository.GetVehiclePasses()
                .Where(a => a.LicensePlate == licensePlate)
                .ToArray()
                .GroupBy(a => a.PassDateTime.Date)
                .Select(group =>
                {
                    var totalTaxAmount = group.Sum(p => p.TollFee);
                    return new
                    {
                        Date = group.Key,
                        TotalTaxAmount = totalTaxAmount > maxAmount ? maxAmount : totalTaxAmount
                    };
                })
                .Sum(a => a.TotalTaxAmount);
        }

        public int GetTollFee(Vehicle vehicle, string licensePlate, DateTime dateTime, int cityId)
        {
            if (IsTollFreeDate(dateTime) || IsTollFreeVehicle(vehicle)) return 0;

            if (IsDailyTaxMaximize(licensePlate, cityId, dateTime)) return 0;

            var time = dateTime.TimeOfDay;
            return _taxRuleService.GetTaxRule(cityId, time)?.Amount ?? 0;
        }

        private bool IsDailyTaxMaximize(string licensePlate, int cityId, DateTime dateTime)
        {
            var totalTodayAmount = _vehiclePassRepository.GetVehiclePasses()
                .Where(a => a.CityId == cityId &&
                            a.LicensePlate == licensePlate &&
                            a.PassDateTime.Date == dateTime.Date)
                .Sum(a => a.TollFee);
            var maxAmount = int.Parse(Constants.MaxAmountPerDayAndVehicle);
            return totalTodayAmount >= maxAmount;
        }

        private static bool IsTollFreeDate(DateTime dateTime)
        {
            if (dateTime.Year != 2013) return false;

            if (dateTime.Month == 7) return true;

            if (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
                return true;

            return GetPublicHolidays().Any(a => a.Date == dateTime.Date);
        }

        private static bool IsTollFreeVehicle(Vehicle vehicle)
        {
            var tollFreeVehicles = new[]
            {
                Vehicle.Bus,
                Vehicle.Diplomat,
                Vehicle.Emergency,
                Vehicle.Foreign,
                Vehicle.Military,
                Vehicle.Motorcycle
            };

            return tollFreeVehicles.Contains(vehicle);
        }

        private static IEnumerable<DateTime> GetPublicHolidays()
        {
            return new[]
            {
                new DateTime(2013, 1, 1),
                new DateTime(2013, 3, 28),
                new DateTime(2013, 3, 29),
                new DateTime(2013, 4, 1),
                new DateTime(2013, 4, 30),
                new DateTime(2013, 5, 1),
                new DateTime(2013, 5, 8),
                new DateTime(2013, 5, 9),
                new DateTime(2013, 6, 5),
                new DateTime(2013, 6, 6),
                new DateTime(2013, 6, 21),
                new DateTime(2013, 11, 1),
                new DateTime(2013, 12, 24),
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2013, 12, 31)
            };
        }
    }
}