
using ProyectoParLibs.Models.Configuracion;
using ProyectoParLibs.Models.Securidad;

namespace ProyectoPAR.Data.Interfaces
{
    public interface IConfigurationService
    {
        
        public Task<IEnumerable<UserLogin>> GetUserLogin();
        public Task<UserLogin> GetUserLogin(int id);        
        public Task<ParametroEmail> GetParametroEmail(int id);        
		public Task<IEnumerable<MenuPrincipal>> GetMenuPrincipal();
		public Task<MenuPrincipal> GetMenuPrincipal(string id);		
        public Task<IEnumerable<OpcionSistema>> GetOpcionSistema();
        public Task<OpcionSistema> GetOpcionSistema(string id);		
        public Task<IEnumerable<Rol>> GetRol();
        public Task<Rol> GetRol(string id);    
        public Task<IEnumerable<CatalogoGenerico>> GetCatalogoGenerico();
        public Task<IEnumerable<CatalogoGenerico>> GetCatalogoGenerico(string nombreCatalogo);
        public Task<CatalogoGenerico> GetCatalogoGenerico(string nombreCatalogo, string id);
        public Task<Parametro> GetParametro();


    }
}
