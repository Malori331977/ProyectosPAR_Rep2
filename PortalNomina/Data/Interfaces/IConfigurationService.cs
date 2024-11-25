
using PortalNominaLibs.Models.Configuracion;
using PortalNominaLibs.Models.Securidad;

namespace PortalNomina.Data.Interfaces
{
    public interface IConfigurationService
    {
        
        public Task<IEnumerable<UserLogin>> GetUserLogin();
        public Task<UserLogin> GetUserLogin(string idnumero);
        

        public Task<IEnumerable<Estado>> GetEstado();
        public Task<Estado> GetEstado(int id);
        
        public Task<ParametroEmail> GetParametroEmail(int id);
        
		public Task<IEnumerable<MenuPrincipal>> GetMenuPrincipal();
		public Task<MenuPrincipal> GetMenuPrincipal(string id);
		
        public Task<IEnumerable<OpcionSistema>> GetOpcionSistema();
        public Task<OpcionSistema> GetOpcionSistema(string id);
		

        public Task<IEnumerable<Rol>> GetRol();
        public Task<Rol> GetRol(string id);
        
        public Task<IEnumerable<Usuario>> GetUsuario();
        public Task<Usuario> GetUsuario(string email);
        public Task<Usuario> GetUsuario(string id, string idnumero);
        
        public Task<IEnumerable<CompaniaConfig>> GetCompaniaConfig();
        public Task<CompaniaConfig> GetCompaniaConfig(string id);

        public Task<IEnumerable<CatalogoGenerico>> GetCatalogoGenerico();
        public Task<IEnumerable<CatalogoGenerico>> GetCatalogoGenerico(string nombreCatalogo);
        public Task<CatalogoGenerico> GetCatalogoGenerico(string nombreCatalogo, string id);

        public Task<Parametro> GetParametro();

        public Task<IEnumerable<CatalogoGenerico>> GetTipoUsuario();


    }
}
