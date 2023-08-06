using System.Linq;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Interfaces.Repositories
{
    public interface ITaxRuleRepository
    {
        IQueryable<TaxRule> GetTaxRules();
        void CreateTaxRule(TaxRule taxRule);
    }
}