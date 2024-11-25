
using PortalNominaLibs.Models.Response;
using PortalNominaLibs.Models.Securidad;

namespace PortalNomina.Data.Interfaces
{
    public interface IEmailRepository
    {
        Task<EventResponse> EnviaCorreoCambioPassword(UserLogin usuario);
        Task<EventResponse> EnviaCorreoInicioSesion(UserLogin usuario);
        
    }
}
