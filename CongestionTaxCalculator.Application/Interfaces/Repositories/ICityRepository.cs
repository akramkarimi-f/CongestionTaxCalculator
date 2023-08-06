using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Interfaces.Repositories
{
    public interface ICityRepository
    {
        void CreateCity(City city);
    }
}