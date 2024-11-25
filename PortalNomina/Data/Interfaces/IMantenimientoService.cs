
using PortalNominaLibs.Models.Mantenimientos;

namespace PortalNomina.Data.Interfaces
{
    public interface IMantenimientoService
    {
        public Task<IEnumerable<Carrera>> GetCarrera();
        public Task<Carrera> GetCarrera(string id);

        
    }
}
