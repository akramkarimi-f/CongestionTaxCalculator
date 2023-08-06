using CongestionTaxCalculator.Application.Interfaces.Repositories;
using CongestionTaxCalculator.Application.Interfaces.Services;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public void AddNewCity(int id, string cityName)
        {
            _cityRepository.CreateCity(new City
            {
                Id = id,
                Name = cityName
            });
        }
    }
}