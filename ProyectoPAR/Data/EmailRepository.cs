using JtSegEncrypta;
using ProyectoPAR.Data.Interfaces;
using ProyectoPAR.Models;
using ProyectoParLibs.Models.Configuracion;
using ProyectoParLibs.Models.Mantenimiento;
using ProyectoParLibs.Models.Response;
using ProyectoParLibs.Models.Securidad;
using System.Collections;
using System.Text;
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

        
              

        public async Task<EventResponse> EnviaCorreoReporteVisita(ReporteExt reporte, byte[] reportePdf, Parametro parametro,string destinatario)
        {
            string passCode = $"{DateTime.Now.ToString("yyyyMMddmmss")}|{reporte.Id}|{reporte.ClienteId}";
            var tokenBytes = Utiles.GetBytes(passCode);
            var token = Convert.ToBase64String(tokenBytes);

            string cuerpoHtml = "<!DOCTYPE html>" +
                "<html><head>Estimado(a), " + reporte.ResponsableCliente + "</head>" +
                $"<body><br><br>Reciba un cordial saludo de parte de {parametro.SiglasCompania}-{parametro.NombreCompania}." +
                "<br><br>De acuerdo a la visita que se acaba de realizar en su empresa, le adjuntamos el informe correspondiente a las actividades realizadas; para su revisión e inmediata aprobación." +
                "<br><br>Le sugerimos conservar este email para cualquier observación o consulta que posteriormente pueda tener." +
                $"<br><br>Si tuviese algún problema al abrir el archivo, sírvase contactarnos al teléfono {parametro.TelefonoCompania} o escríbanos a {parametro.EmailCompania} y con gusto le atenderemos." +
                "<br><br>Si esta de acuerdo, por favor, ingrese al siguiente enlace para la autorización del reporte. " +
                $"<br><br><a href='{parametro.UrlAplicacion}/confirmacionreporte/{token}'>Haga clic aquí para confirmar el reporte.</a>" +
                "<br><br>En caso de que considere que no se debe de aprobar el trabajo realizado, por favor comuniquese con nosotros." +
                "<br><br>De antemano le agradecemos la confianza depositada en nuestros servicios." +
                "<br><br>Atentamente," +
                $"<br><br><br><br>{parametro.SiglasCompania}-{parametro.NombreCompania}<br>{parametro.DireccionCompania}<br>{parametro.EmailCompania}<br>{parametro.TelefonoCompania}" +
                "</body><footer><br><br><hr>" +
                "Este mensaje se generó de forma automática.  Por favor no contestar.</footer></html>";

            Email email = new();
            email.Asunto = $"Informe de Visita {reporte.Id} para {reporte.Cliente!.Nombre}";
            email.Cuerpo = cuerpoHtml;
            email.Para = destinatario;
            email.Adjunto = $"ReporteVisita{reporte.Id}.pdf";
            email.CC = "";
            email.StreamAdjunto = reportePdf;
            email.De = "default";
            email.SmtpServer = "default";
            email.NombrePara = reporte.ResponsableCliente;

            List<Email> correoPorEnviar = new List<Email> { email };

            EventResponse respuesta = new();
            respuesta = await _genericService.Post("EnviarNotificacion", correoPorEnviar);

            return respuesta;
        }

    }

}
