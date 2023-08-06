using CongestionTaxCalculator.Application.Interfaces.Repositories;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly TaxContext _context;

        public CityRepository(TaxContext context)
        {
            _context = context;
        }

        public void CreateCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
        }
    }
}