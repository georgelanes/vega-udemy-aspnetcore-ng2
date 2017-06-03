using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Models;
using Vega.Controllers.Resources;
using Vega.Persistence;

namespace Vega.Controllers
{
    
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MakesController
    {

        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public MakesController(VegaDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes() {
            var makes = await _context.Makes
                .Include("Models")
                .ToListAsync();
                            
            var listVieModel = Mapper.Map<IEnumerable<Make>,IEnumerable<MakeResource>>(makes);
            return listVieModel;
        }
    }
}