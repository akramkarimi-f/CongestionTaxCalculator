using System.Linq;
using CongestionTaxCalculator.Application.Interfaces.Repositories;
using CongestionTaxCalculator.Domain;

namespace CongestionTaxCalculator.Infrastructure.Repositories
{
    public class VehiclePassRepository : IVehiclePassRepository
    {
        private readonly TaxContext _context;

        public VehiclePassRepository(TaxContext context)
        {
            _context = context;
        }

        public IQueryable<VehiclePass> GetVehiclePasses()
        {
            return _context.VehiclePasses.AsQueryable();
        }

        public void AddNewVehiclePass(VehiclePass pass)
        {
            _context.VehiclePasses.Add(pass);
            _context.SaveChanges();
        }

        public void UpdateVehiclePass(VehiclePass pass)
        {
            var vehiclePass = _context.VehiclePasses.Find(pass.Id);
            if (vehiclePass == null) return;

            vehiclePass.TollFee = pass.TollFee;
            vehiclePass.LastUpdate = pass.LastUpdate;

            _context.SaveChanges();
        }
    }
}