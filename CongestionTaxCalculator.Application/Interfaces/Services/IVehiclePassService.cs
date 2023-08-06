using System;
using CongestionTaxCalculator.Domain;
using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Application.Interfaces.Services
{
    public interface IVehiclePassService
    {
        VehiclePass GetLastPassOfTheVehicle(string licensePlate, int cityId);
        void AddNewVehiclePass(Vehicle vehicle, string licensePlate, DateTime passDateTime, int cityId);
    }
}