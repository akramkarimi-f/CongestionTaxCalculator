using System;
using System.Linq;
using CongestionTaxCalculator.Application.Interfaces;
using CongestionTaxCalculator.Application.Interfaces.Repositories;
using CongestionTaxCalculator.Application.Interfaces.Services;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Application.Services
{
    public class VehiclePassService : IVehiclePassService
    {
        private readonly ICongestionTaxCalculator _congestionTaxCalculator;
        private readonly IVehiclePassRepository _repository;

        public VehiclePassService(
            IVehiclePassRepository repository,
            ICongestionTaxCalculator congestionTaxCalculator)
        {
            _repository = repository;
            _congestionTaxCalculator = congestionTaxCalculator;
        }

        public VehiclePass GetLastPassOfTheVehicle(string licensePlate, int cityId)
        {
            return _repository.GetVehiclePasses()
                .Where(a => a.CityId == cityId && a.LicensePlate == licensePlate)
                .OrderByDescending(a => a.PassDateTime)
                .FirstOrDefault();
        }

        public void AddNewVehiclePass(Vehicle vehicle, string licensePlate, DateTime passDateTime, int cityId)
        {
            var tollFee = _congestionTaxCalculator.GetTollFee(vehicle, licensePlate, passDateTime, cityId);

            var lastPassOfTheVehicle = GetLastPassOfTheVehicle(licensePlate, cityId);

            if (lastPassOfTheVehicle == null || lastPassOfTheVehicle.PassDateTime < passDateTime.AddHours(-1))
            {
                _repository.AddNewVehiclePass(new VehiclePass
                {
                    Vehicle = vehicle,
                    LicensePlate = licensePlate,
                    TollFee = tollFee,
                    PassDateTime = passDateTime,
                    CityId = cityId
                });
            }
            else if (tollFee > lastPassOfTheVehicle.TollFee)
            {
                lastPassOfTheVehicle.TollFee = tollFee;
                lastPassOfTheVehicle.LastUpdate = passDateTime;

                _repository.UpdateVehiclePass(lastPassOfTheVehicle);
            }
        }
    }
}