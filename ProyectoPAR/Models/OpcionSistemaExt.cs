using ProyectoParLibs.Models.Configuracion;
namespace ProyectoPAR.Models
{
    public class OpcionSistemaExt : OpcionSistema
    {
        public string DescMenuPrincipal { get; set; } = "";
        public bool Habilitado { get; set; }
    }
}
