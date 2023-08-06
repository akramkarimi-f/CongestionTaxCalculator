using CongestionTaxCalculator.Domain;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Infrastructure
{
    public class TaxContext : DbContext
    {
        public TaxContext(DbContextOptions<TaxContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<TaxRule> TaxRules { get; set; }
        public DbSet<VehiclePass> VehiclePasses { get; set; }
    }
}