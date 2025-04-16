using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Models
{
    public class ContratoExt:Contrato
    {
        public string? NombreCliente { get; set; }
        public string? DescEstado { get; set; }
        public string? DescNivelContrato { get; set; }
        public string? DescFormaPago { get; set; }
        public decimal HorasPendientes { get; set; }
        public string? DesctipoContrato { get; set; }

    }
}
