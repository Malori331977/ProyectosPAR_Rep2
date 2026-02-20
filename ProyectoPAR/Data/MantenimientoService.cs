
using ProyectoPAR.Data.Interfaces;
using ProyectoParLibs.Models.Mantenimiento;

namespace ProyectoPAR.Data
{
    public class MantenimientoService : IMantenimientoService
	{
		private readonly ILogger<MantenimientoService> _logger;
		private string InterfaceName = "MantenimientoService";
        private readonly IGenericService _genericService;
        public MantenimientoService(ILogger<MantenimientoService> logger, IGenericService genericService) {
			_logger = logger;
			_genericService = genericService;
		}

		public async Task<IEnumerable<Consultor>> GetConsultor()
		{
			IEnumerable<Consultor>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Consultor/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<Consultor>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<Consultor>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<Consultor> GetConsultor(int id)
		{
            Consultor? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Consultor/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<Consultor>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

        public async Task<Consultor> GetConsultorByUsuario(string id)
        {
            Consultor? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ConsultorByUsuario/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Consultor>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetConsultorByUsuario: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Cliente>> GetCliente()
        {
            IEnumerable<Cliente>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Cliente/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Cliente>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Cliente>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetCliente: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Cliente> GetCliente(string id)
        {
            Cliente? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Cliente/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Cliente>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetCliente: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Tarea>> GetTarea()
        {
            IEnumerable<Tarea>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Tarea/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Tarea>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Tarea>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTarea: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Tarea> GetTarea(int id)
        {
            Tarea? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Tarea/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Tarea>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTarea: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Producto>> GetProducto()
        {
            IEnumerable<Producto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Producto/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Producto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Producto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProducto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Producto> GetProducto(int id)
        {
            Producto? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Producto/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Producto>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProducto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Partner>> GetPartner()
        {
            IEnumerable<Partner>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Partner/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Partner>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Partner>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetPartner: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Partner> GetPartner(int id)
        {
            Partner? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Partner/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Partner>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetPartner: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }
    }
}
