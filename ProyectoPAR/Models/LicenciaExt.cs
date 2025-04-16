using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Models
{
    public class LicenciaExt:Licencia
    {
        public string ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public string DescProducto { get; set; }
        public string DescEstado { get; set; }
        public string AliasCliente { get; set; }
    }
}
