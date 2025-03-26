

using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Data.Interfaces
{
    public interface IProcesoService
    {
        public Task<IEnumerable<Reporte>> GetReporte();
        public Task<Reporte> GetReporte(int id);
        public Task<IEnumerable<Reporte>> GetReporteConsultor(int id);
        public Task<IEnumerable<Reporte>> GetReporte(string estado, string fechaInicial, string fechaFinal);
        
    }
}
