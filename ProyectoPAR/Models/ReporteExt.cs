using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Models
{
    public class ReporteExt:Reporte
    {
        public string NombreConsultor { get; set; }
        public string NombreCliente { get; set; }
        public string DescEstado { get; set; }
        public string DescTipoReporte { get; set; }
        public string DescProyecto { get; set; }
        public string DescTarea { get; set; }
        public TimeOnly? HoraInicioTarea { get; set; }
        public TimeOnly? HoraFinTarea { get; set; }
        public DateTime? FechaPlan { get; set; }
    }
}
