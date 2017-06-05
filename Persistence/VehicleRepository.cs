using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;
using Vega.Core;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega.Extensions;

namespace Vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext _context;
        public VehicleRepository(VegaDbContext context)
        {
            this._context = context;
        }

        public async Task<QueryResult<Vehicle>> GetAllAsync(VehicleQuery queryObj)
        {
            var queryResult = new QueryResult<Vehicle>();
            var query = _context.Vehicles
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature).AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

            if (queryObj.ModelId.HasValue)
                query = query.Where(v => v.Model.Id == queryObj.ModelId.Value);


            var collumnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.name,
                ["model"] = v => v.Model.name,
                ["contactName"] = v => v.ContactName
            };

            query = query.ApplyOrdering(queryObj, collumnsMap);

            queryResult.TotalItems = await query.CountAsync();
            query = query.ApplyPagging(queryObj);
            queryResult.Items = await query.ToListAsync();

            return queryResult;

        }

        

        public void Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);

        }

        public async Task<Vehicle> Find(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> GetVehicle(int id)
        {
            return await _context.Vehicles
                .Include(f => f.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
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