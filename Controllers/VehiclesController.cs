using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vega.Models;
using Vega.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public VehiclesController(IMapper mapper, VegaDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = Mapper.Map<VehicleViewModel, Vehicle>(viewModel);
                vehicle.LastUpdate = DateTime.Now;

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                var result = Mapper.Map<Vehicle, VehicleViewModel>(vehicle);


                return Ok(vehicle);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle([FromBody]int id, VehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = await _context.Vehicles.Include(f => f.Features).SingleOrDefaultAsync(v => v.Id == id);
                if (vehicle == null)
                    return NotFound("vehicule not found");

                Mapper.Map<VehicleViewModel, Vehicle>(viewModel, vehicle);
                vehicle.LastUpdate = DateTime.Now;

                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                var result = Mapper.Map<Vehicle, VehicleViewModel>(vehicle);


                return Ok(vehicle);
            }
            return BadRequest(viewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound("vehicule not found");

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.Include(f => f.Features).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
                return NotFound("vehicule not found");

            return Ok(vehicle);
        }
    }
}