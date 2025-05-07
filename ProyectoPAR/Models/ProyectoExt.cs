using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Models
{
    public class ProyectoExt:Proyecto
    {
        public string? NombreCliente { get; set; }
        public string? DescEstado { get; set; }
        public string? DescEtapa { get; set; }
    }
}
