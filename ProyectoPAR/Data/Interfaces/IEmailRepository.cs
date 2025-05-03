
using ProyectoPAR.Models;
using ProyectoParLibs.Models.Configuracion;
using ProyectoParLibs.Models.Mantenimiento;
using ProyectoParLibs.Models.Response;
using ProyectoParLibs.Models.Securidad;

namespace ProyectoPAR.Data.Interfaces
{
    public interface IEmailRepository
    {
        Task<EventResponse> EnviaCorreoCambioPassword(UserData usuario);
        Task<EventResponse> EnviaCorreoInicioSesion(UserData usuario);
        Task<EventResponse> EnviaCorreoReporteVisita(ReporteExt reporte, byte[] reportePdf, Parametro parametro,string destinatario);


    }
}
