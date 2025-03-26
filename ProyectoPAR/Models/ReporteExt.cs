using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Models
{
    public class ReporteExt:Reporte
    {
        public string NombreConsultor { get; set; }
        public string NombreCliente { get; set; }
        public string DescEstado { get; set; }
        public string DescTipoReporte { get; set; }

    }
}
