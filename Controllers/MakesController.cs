using System.Collections.Generic;
using System.Linq;
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
    public class MakesController
    {

        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public MakesController(VegaDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeViewModel>> GetMakes() {
            var makes = await _context.Makes
                .Include("Models")
                .ToListAsync();
                            
            var listVieModel = Mapper.Map<IEnumerable<Make>,IEnumerable<MakeViewModel>>(makes);
            return listVieModel;
        }
    }
}