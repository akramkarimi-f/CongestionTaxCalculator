using System.Linq;
using CongestionTaxCalculator.Application.Interfaces.Repositories;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Infrastructure.Repositories
{
    public class TaxRuleRepository : ITaxRuleRepository
    {
        private readonly TaxContext _context;

        public TaxRuleRepository(TaxContext context)
        {
            _context = context;
        }

        public IQueryable<TaxRule> GetTaxRules()
        {
            return _context.TaxRules;
        }

        public void CreateTaxRule(TaxRule taxRule)
        {
            _context.TaxRules.Add(taxRule);
            _context.SaveChanges();
        }
    }
}