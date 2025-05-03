namespace ProyectoPAR.Models
{
    public class FiltroReporte
    {
        public DateTime FechaInicial { get; set; } = DateTime.Now.AddDays(-30);
        public DateTime FechaFinal { get; set; } = DateTime.Now;
        public string EstadoId { get; set; } = "E";
    }
}
