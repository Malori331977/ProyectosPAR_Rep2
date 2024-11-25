using PortalNomina.Data.Interfaces;
using PortalNominaLibs.Models.Configuracion;
using PortalNominaLibs.Models.Securidad;

namespace PortalNomina.Data
{
    public class ConfigurationService : IConfigurationService
	{
		private readonly ILogger<ConfigurationService> _logger;
		private string InterfaceName = "ConfigurationService";
        private readonly IGenericService _genericService;
        public ConfigurationService(ILogger<ConfigurationService> logger, IGenericService genericService) {
			_logger = logger;
			_genericService = genericService;
		}
	
		public async Task<IEnumerable<CompaniaConfig>> GetCompaniaConfig()
		{
			IEnumerable<CompaniaConfig>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/CompaniaConfig/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<CompaniaConfig>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<CompaniaConfig>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetCompaniaConfig: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<CompaniaConfig> GetCompaniaConfig(string id)
		{
			CompaniaConfig? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/CompaniaConfig/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<CompaniaConfig>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetCompaniaConfig: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<IEnumerable<Estado>> GetEstado()
		{
			IEnumerable<Estado>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Estado/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<Estado>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<Estado>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetEstado: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<Estado> GetEstado(int id)
		{
			Estado? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Estado/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<Estado>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetUserLogin: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<IEnumerable<UserLogin>> GetUserLogin()
		{
			IEnumerable<UserLogin>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/UserLogin/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<UserLogin>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<UserLogin>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetUserLogin: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<UserLogin> GetUserLogin(string idnumero)
		{
			UserLogin? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/UserLogin/{idnumero}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<UserLogin>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetUserLogin: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<IEnumerable<MenuPrincipal>> GetMenuPrincipal()
		{
			IEnumerable<MenuPrincipal>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/MenuPrincipal/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<MenuPrincipal>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<MenuPrincipal>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetMenuPrincipal: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<MenuPrincipal> GetMenuPrincipal(string id)
		{
			MenuPrincipal? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/MenuPrincipal/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<MenuPrincipal>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetMenuPrincipal: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<IEnumerable<OpcionSistema>> GetOpcionSistema()
		{
			IEnumerable<OpcionSistema>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/OpcionSistema/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<OpcionSistema>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<OpcionSistema>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetOpcionSistema: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<OpcionSistema> GetOpcionSistema(string id)
		{
			OpcionSistema? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/OpcionSistema/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<OpcionSistema>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetOpcionSistema: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<ParametroEmail> GetParametroEmail(int id)
		{
			ParametroEmail? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/ParametroEmail/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<ParametroEmail>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetParametroEmail: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<IEnumerable<Rol>> GetRol()
		{
			IEnumerable<Rol>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Rol";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<Rol>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<Rol>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetRol: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<Rol> GetRol(string id)
		{
			Rol? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Rol/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<Rol>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetRol: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<IEnumerable<Usuario>> GetUsuario()
		{
			IEnumerable<Usuario>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Usuario/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<Usuario>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<Usuario>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetRol: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<Usuario> GetUsuario(string email)
		{
			Usuario? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Usuario/{email}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<Usuario>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetUsuario: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<Usuario> GetUsuario(string id, string idnumero)
		{
			Usuario? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Usuario/{id}/{idnumero}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<Usuario>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetUsuario: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}



        public async Task<IEnumerable<CatalogoGenerico>> GetCatalogoGenerico()
        {
            IEnumerable<CatalogoGenerico>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/CatalogoGenerico/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<CatalogoGenerico>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<CatalogoGenerico>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetCatalogoGenerico: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<CatalogoGenerico>> GetCatalogoGenerico(string nombreCatalogo)
        {
            IEnumerable<CatalogoGenerico>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/CatalogoGenerico/{nombreCatalogo}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<CatalogoGenerico>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetCatalogoGenerico: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<CatalogoGenerico> GetCatalogoGenerico(string nombreCatalogo, string id)
        {
            CatalogoGenerico? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/CatalogoGenerico/{nombreCatalogo}/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<CatalogoGenerico>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetCatalogoGenerico: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Parametro> GetParametro()
        {
            Parametro? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Parametro";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Parametro>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetParametro: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<CatalogoGenerico>> GetTipoUsuario()
        {
            IEnumerable<CatalogoGenerico>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoUsuario/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<CatalogoGenerico>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<CatalogoGenerico>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoUsuario: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }
    }
}
