
using ProyectoParLibs.Models.Mantenimiento;

namespace ProyectoPAR.Data.Interfaces
{
    public interface IMantenimientoService
    {
        public Task<IEnumerable<Consultor>> GetConsultor();
        public Task<Consultor> GetConsultor(int id);
        public Task<Consultor> GetConsultorByUsuario(string id);
        public Task<IEnumerable<Cliente>> GetCliente();
        public Task<Cliente> GetCliente(string id);
        public Task<IEnumerable<Tarea>> GetTarea();
        public Task<Tarea> GetTarea(int id);
        public Task<IEnumerable<Producto>> GetProducto();
        public Task<Producto> GetProducto(int id);
        public Task<IEnumerable<Partner>> GetPartner();
        public Task<Partner> GetPartner(int id);
    }
}
