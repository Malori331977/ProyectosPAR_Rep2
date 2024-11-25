
using PortalNomina.Data.Interfaces;
using PortalNominaLibs.Models.Mantenimientos;

namespace PortalNomina.Data
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

		public async Task<IEnumerable<Carrera>> GetCarrera()
		{
			IEnumerable<Carrera>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Carrera/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<Carrera>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<Carrera>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetCarrera: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<Carrera> GetCarrera(string id)
		{
			Carrera? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Carrera/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<Carrera>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetCarrera: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

        

    }
}
