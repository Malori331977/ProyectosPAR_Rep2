namespace ProyectoPAR.Models
{
    public class Appointment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; }
        public int AgendaId { get; set; }
        public string Background { get; set; }
        public string Color { get; set; }
        public int Modo { get; set; }
        public int ProyectoId { get; set; }
        public int TareaId { get; set; }
        public int ItemId { get; set; }

        public TimeOnly HoraInicioReal { get; set; }
        public TimeOnly HoraFinalReal { get; set; }
        public string Observaciones { get; set; }
        public string EstadoId { get; set; }



    }
}
