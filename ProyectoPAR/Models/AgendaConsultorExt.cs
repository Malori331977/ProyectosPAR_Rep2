using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Models
{
    public class AgendaConsultorExt:AgendaConsultor
    {
        public string? NombreConsultor { get; set; }
        public string DescProyecto { get; set; }
        public string DescTarea { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
