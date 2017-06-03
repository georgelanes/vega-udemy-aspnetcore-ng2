using System.Collections.Generic;
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
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public FeaturesController(VegaDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetMakes() {
            var features=await _context.Features.ToListAsync();
            var listViewModel = Mapper.Map<IEnumerable<Feature>,IEnumerable<KeyValuePairResource>>(features);
            return listViewModel;
        }
    }
}