using PortalNominaLibs.Models.Configuracion;
namespace PortalNomina.Models
{
    public class OpcionSistemaExt : OpcionSistema
    {
        public string DescMenuPrincipal { get; set; } = "";
        public bool Habilitado { get; set; }
    }
}
