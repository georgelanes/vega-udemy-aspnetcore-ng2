using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id);
         void Add(Vehicle vehicle);
         void Update(Vehicle vehicle);
         void Remove(int id);
         Task<Vehicle> Find(int id);
    }
}