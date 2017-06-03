using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;
using Vega.Core;

namespace Vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext _context;
        public VehicleRepository(VegaDbContext context)
        {
            this._context = context;
        }

        public void Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);

        }

        public async Task<Vehicle> Find(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> GetVehicle(int id){
            return await _context.Vehicles
                .Include(f => f.Features)
                    .ThenInclude(vf=>vf.Feature)
                .Include(v=>v.Model)
                    .ThenInclude(m=>m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Remove(int id)
        {
            var vehicle = _context.Vehicles.Find(id);
            _context.Vehicles.Remove(vehicle);

        }

        public void Update(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);

        }

    
    }
}