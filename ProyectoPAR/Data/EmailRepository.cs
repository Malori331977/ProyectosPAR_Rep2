using ProyectoPAR.Data.Interfaces;
using ProyectoPAR.Models;
using ProyectoParLibs.Models.Mantenimiento;
using ProyectoParLibs.Models.Response;
using ProyectoParLibs.Models.Securidad;
using Email = ProyectoParLibs.Models.Configuracion.Email;

namespace ProyectoPAR.Data
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericService _genericService;
        private readonly IConfigurationService _configurationService;
        private string ulrPortal;
        public EmailRepository(IConfiguration configuration, IGenericService genericService, IConfigurationService configurationService)
        {
            _configuration = configuration;
            _genericService = genericService;
            _configurationService = configurationService;
        }

        public async Task<EventResponse> EnviaCorreoCambioPassword(UserData usuario)
        {
            string cuerpoHtml = "<!DOCTYPE html>" +
                "<html><head>Hola, " + usuario.consultor!.Nombre + "</head>" +
                "<body><br><br>Se solicitó recientemente cambiar la contraseña de su cuenta en el Sistema PAR. Si usted solicitó este cambio de contraseña, utilice el código siguiente para establecer una nueva contraseña." +
                "<br>El código de seguridad que debe indicar en la aplicación es el siguiente: " +
                "<br><h3>" + usuario.userLogin!.CodigoSeguridad.Trim() + "</h3><br><br>" +
                "<br><br>Si no desea cambiar su contraseña, ignore este mensaje.</body><footer><br><hr>" +
                "Este mensaje se generó de forma automática.  Por favor no contestar.</footer></html>";

            Email email = new();
            email.Asunto = "Confirmación del restablecimiento de la contraseña";
            email.Cuerpo = cuerpoHtml;
            email.Para = usuario.consultor.Email!;
            email.Adjunto = "";
            email.CC = "";
            email.StreamAdjunto = new byte[0];
            email.De = "default";
            email.SmtpServer = "default";

            List<Email> correoPorEnviar = new List<Email> { email };

            EventResponse respuesta = new();
            respuesta = await _genericService.Post("EnviarNotificacion", correoPorEnviar);

            return respuesta;
        }

        public async Task<EventResponse> EnviaCorreoInicioSesion(UserData usuario)
        {

            string cuerpoHtml = "<!DOCTYPE html>" +
                "<html><head>Hola, " + usuario.consultor!.Nombre + "</head>" +
                "<body><br><br>Se está solicitando el inicio de sesión en el Sistema PAR. Si usted está realizando esta solicitud por favor utilice el código siguiente para poder ingresar al sistema." +
                "<br>El código de seguridad que debe indicar en la aplicación es el siguiente: " +
                "<br><h3>" + usuario.userLogin!.CodigoSeguridad.Trim() + "</h3><br><br>" +
                "Este mensaje se generó de forma automática.  Por favor no contestar.</footer></html>";

            Email email = new();
            email.Asunto = "Código de seguridad para el Inicio de Sesión en el Portal de AutoGestión.";
            email.Cuerpo = cuerpoHtml;
            email.Para = usuario.consultor!.Email!;
            email.Adjunto = "";
            email.CC = "";
            email.StreamAdjunto = new byte[0];
            email.De = "default";
            email.SmtpServer = "default";

            List<Email> correoPorEnviar = new List<Email> { email };
            EventResponse respuesta = new();
            respuesta = await _genericService.Post("EnviarNotificacion", correoPorEnviar);

            return respuesta;
        }

    }

}
