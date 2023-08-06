using System;
using CongestionTaxCalculator.Application.Interfaces;
using CongestionTaxCalculator.Application.Interfaces.Services;
using CongestionTaxCalculator.ConsoleApp.Resources;
using CongestionTaxCalculator.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.ConsoleApp
{
    internal partial class Program
    {
        private readonly ICityService _cityService;
        private readonly ICongestionTaxCalculator _congestionTaxCalculator;
        private readonly ITaxRuleService _taxRuleService;
        private readonly IVehiclePassService _vehiclePassService;

        private Program(
            ICongestionTaxCalculator congestionTaxCalculator,
            ITaxRuleService taxRuleService,
            ICityService cityService,
            IVehiclePassService vehiclePassService)
        {
            _congestionTaxCalculator = congestionTaxCalculator;
            _taxRuleService = taxRuleService;
            _cityService = cityService;
            _vehiclePassService = vehiclePassService;
        }

        private static Program CreateProgramInstance()
        {
            try
            {
                var serviceProvider = ConfigureServices();

                var congestionTaxCalculator = serviceProvider.GetService<ICongestionTaxCalculator>();
                var taxRuleService = serviceProvider.GetService<ITaxRuleService>();
                var cityService = serviceProvider.GetService<ICityService>();
                var vehiclePassService = serviceProvider.GetService<IVehiclePassService>();


                return new Program(congestionTaxCalculator, taxRuleService, cityService, vehiclePassService);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ProgramInstanceCreationErrorMessageDetails, ex.Message);
                return null;
            }
        }

        private decimal CalculateCongestionTax(string licensePlate)
        {
            return _congestionTaxCalculator.GetTax(licensePlate);
        }

        private void AddNewCity(int id, string name)
        {
            _cityService.AddNewCity(id, name);
        }

        private void AddNewTaxRule(int cityId, TimeSpan startTime, TimeSpan endTime, int amount)
        {
            _taxRuleService.AddNewTaxRule(cityId, startTime, endTime, amount);
        }

        private void AddNewVehiclePass(Vehicle vehicle, string licensePlate, DateTime passDateTime, int cityId)
        {
            _vehiclePassService.AddNewVehiclePass(vehicle, licensePlate, passDateTime, cityId);
        }
    }
}