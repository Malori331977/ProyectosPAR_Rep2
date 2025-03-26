
using ProyectoPAR.Data.Interfaces;
using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Data
{
    public class ProcesoService : IProcesoService
    {
		private readonly ILogger<ProcesoService> _logger;
		private string InterfaceName = "ProcesoService";
        private readonly IGenericService _genericService;
        public ProcesoService(ILogger<ProcesoService> logger, IGenericService genericService) {
			_logger = logger;
			_genericService = genericService;
		}

		public async Task<IEnumerable<Reporte>> GetReporte()
		{
			IEnumerable<Reporte>? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Reporte/";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<IList<Reporte>>();
					readJob.Wait();
					model = readJob.Result;
				}
			}
			catch (Exception e)
			{
				//devuelve el codigo de error
				model = Enumerable.Empty<Reporte>();
				string error = e.InnerException is null ? e.Message : e.InnerException.Message;
				_logger.LogError($"{InterfaceName}.GetReporte: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
			}
			return model!;
		}

		public async Task<Reporte> GetReporte(int id)
		{
            Reporte? model = null;
			HttpResponseMessage? result = null;
			try
			{
				var method = $"/Reporte/{id}";
				result = await _genericService.Get(method);

				if (result.IsSuccessStatusCode)
				{
					var readJob = result.Content.ReadFromJsonAsync<Reporte>();
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

        public async Task<IEnumerable<Reporte>> GetReporte(string estado, string fechaInicio, string fechaFinal)
        {
            IEnumerable<Reporte>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Reporte/{estado}/{fechaInicio}/{fechaFinal}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Reporte>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Reporte>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetReporte: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Reporte>> GetReporteConsultor(int id)
        {
            IEnumerable<Reporte>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ReporteConsultor/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Reporte>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Reporte>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetReporteConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }


        
    }
}
