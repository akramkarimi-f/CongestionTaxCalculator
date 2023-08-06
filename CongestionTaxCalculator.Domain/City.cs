using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculator.Domain
{
    public class City
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TaxRule> TaxRules { get; set; }
    }
}