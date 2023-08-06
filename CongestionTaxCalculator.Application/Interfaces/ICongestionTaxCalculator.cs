using System;
using CongestionTaxCalculator.Domain.Enums;

namespace CongestionTaxCalculator.Application.Interfaces
{
    public interface ICongestionTaxCalculator
    {
        int GetTax(string licensePlate);
        int GetTollFee(Vehicle vehicle, string licensePlate, DateTime dateTime, int cityId);
    }
}