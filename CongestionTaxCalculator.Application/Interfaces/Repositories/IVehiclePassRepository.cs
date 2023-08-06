using System.Linq;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Application.Interfaces.Repositories
{
    public interface IVehiclePassRepository
    {
        IQueryable<VehiclePass> GetVehiclePasses();
        void AddNewVehiclePass(VehiclePass pass);
        void UpdateVehiclePass(VehiclePass pass);
    }
}