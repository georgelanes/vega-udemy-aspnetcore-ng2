using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vega.Core.Models;
using Vega.Controllers.Resources;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Persistence;
using Vega.Core;

namespace Vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filter)
        {
            var _filter = _mapper.Map<VehicleQueryResource, VehicleQuery>(filter);
            var queryResult = await repository.GetAllAsync(_filter);
            var view = _mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);
            return view;

        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource Resourcel)
        {

            if (ModelState.IsValid)
            {
                var vehicle = Mapper.Map<SaveVehicleResource, Vehicle>(Resourcel);
                vehicle.LastUpdate = DateTime.Now;

                repository.Add(vehicle);
                await unitOfWork.CompleteAsync();

                vehicle = await repository.GetVehicle(vehicle.Id);

                var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);


                return Ok(vehicle);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (ModelState.IsValid)
            {
                var vehicle = await repository.GetVehicle(id);
                if (vehicle == null)
                    return NotFound("vehicule not found");

                Mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
                vehicle.LastUpdate = DateTime.Now;

                repository.Update(vehicle);
                await unitOfWork.CompleteAsync();

                vehicle = await repository.GetVehicle(vehicle.Id);
                var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);


                return Ok(vehicle);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {

            var vehicle = await repository.Find(id);
            if (vehicle == null)
                return NotFound("vehicule not found");

            repository.Remove(id);
            await unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound("vehicule not found");

            var vehicleResource = Mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}