using System;
using System.Linq;
using CongestionTaxCalculator.Application.Interfaces.Repositories;
using CongestionTaxCalculator.Application.Interfaces.Services;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Services
{
    public class TaxRuleService : ITaxRuleService
    {
        private readonly ITaxRuleRepository _taxRuleRepository;

        public TaxRuleService(ITaxRuleRepository taxRuleRepository)
        {
            _taxRuleRepository = taxRuleRepository;
        }

        public TaxRule GetTaxRule(int cityId, TimeSpan time)
        {
            return _taxRuleRepository.GetTaxRules()
                .Where(a => a.CityId == cityId)
                .AsEnumerable()
                .FirstOrDefault(a => time >= a.StartTime && time < a.EndTime);
        }

        public void AddNewTaxRule(int cityId, TimeSpan startTime, TimeSpan endTime, int amount)
        {
            _taxRuleRepository.CreateTaxRule(
                new TaxRule
                {
                    CityId = cityId,
                    StartTime = startTime,
                    EndTime = endTime,
                    Amount = amount
                });
        }
    }
}