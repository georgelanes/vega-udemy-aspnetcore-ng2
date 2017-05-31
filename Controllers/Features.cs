using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Models;
using Vega.ViewModels;

namespace Vega.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class Features : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public Features(VegaDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet("/api/features")]
        public async Task<IEnumerable<FeatureViewModel>> GetMakes() {
            var features=await _context.Features.ToListAsync();
            var listViewModel = Mapper.Map<IEnumerable<Feature>,IEnumerable<FeatureViewModel>>(features);
            return listViewModel;
        }
    }
}