using System;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Interfaces.Services
{
    public interface ITaxRuleService
    {
        TaxRule GetTaxRule(int cityId, TimeSpan time);
        void AddNewTaxRule(int cityId, TimeSpan startTime, TimeSpan endTime, int amount);
    }
}