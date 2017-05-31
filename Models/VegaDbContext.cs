using Microsoft.EntityFrameworkCore;

namespace Vega.Models
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) 
            : base(options)
        {
        }
        public DbSet<Make> Makes  { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}