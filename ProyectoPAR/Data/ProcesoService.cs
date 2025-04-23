
using DocumentFormat.OpenXml.Office2010.Excel;
using ProyectoPAR.Data.Interfaces;
using ProyectoParLibs.Models.Mantenimiento;
using ProyectoParLibs.Models.Procesos;
using System.Threading;

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

        public async Task<IEnumerable<Adendum>> GetAdendum()
        {
            IEnumerable<Adendum>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Adendum/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Adendum>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Adendum>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetAdendum: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Adendum>> GetAdendum(int idContrato)
        {
            IEnumerable<Adendum>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Adendum/{idContrato}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Adendum>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Adendum>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetAdendum: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Adendum> GetAdendum(int idContrato, int id)
        {
            Adendum? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Adendum/{idContrato}/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Adendum>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetAdendum: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<AgendaConsultor>> GetAgendaConsultor()
        {
            IEnumerable<AgendaConsultor>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/AgendaConsultor";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<AgendaConsultor>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<AgendaConsultor>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetAgendaConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<AgendaConsultor> GetAgendaConsultor(int id)
        {
            AgendaConsultor? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/AgendaConsultor/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<AgendaConsultor>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetAgendaConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<AgendaConsultor>> GetAgendaConsultorByProyecto(int proyectoId)
        {
            IEnumerable<AgendaConsultor>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/AgendaConsultorByProyecto/{proyectoId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<AgendaConsultor>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<AgendaConsultor>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetAgendaConsultorByProyecto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<AgendaConsultor>> GetAgendaConsultorByConsultor(int consultorId)
        {
            IEnumerable<AgendaConsultor>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/AgendaConsultorByConsultor/{consultorId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<AgendaConsultor>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<AgendaConsultor>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetAgendaConsultorByConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<AgendaConsultor> GetAgendaConsultorByConsultor(int id, int consultorId)
        {
            AgendaConsultor? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/AgendaConsultorByConsultor/{id}/{consultorId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<AgendaConsultor>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetAgendaConsultorByConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Contrato>> GetContrato()
        {
            IEnumerable<Contrato>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Contrato/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Contrato>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Contrato>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetContrato: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Contrato> GetContrato(int id)
        {
            Contrato? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Contrato/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Contrato>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetContrato: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Contrato>> GetContratoByEstado(string estado)
        {
            IEnumerable<Contrato>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ContratoByEstado/{estado}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Contrato>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Contrato>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetContratoByEstado: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Licencia>> GetLicencia()
        {
            IEnumerable<Licencia>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Licencia/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Licencia>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Licencia>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetLicencia: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Licencia>> GetLicencia(int productoId, int contratoId)
        {
            IEnumerable<Licencia>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Licencia/{productoId}/{contratoId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Licencia>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Licencia>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetLicencia: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Licencia> GetLicencia(int id)
        {
            Licencia? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Licencia/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Licencia>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetLicencia: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Proyecto>> GetProyecto()
        {
            IEnumerable<Proyecto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Proyecto/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Proyecto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Proyecto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyecto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Proyecto> GetProyecto(int id)
        {
            Proyecto? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Proyecto/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Proyecto>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyecto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Proyecto>> GetProyectoByEstado(string estado)
        {
            IEnumerable<Proyecto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoByEstado/{estado}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Proyecto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Proyecto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoByEstado: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Proyecto>> GetProyectoByConsultor(int consultor)
        {
            IEnumerable<Proyecto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoByConsultor/{consultor}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Proyecto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Proyecto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.ProyectoByConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoAvance>> GetProyectoAvance()
        {
            IEnumerable<ProyectoAvance>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoAvance/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoAvance>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoAvance>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoAvance: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoAvance>> GetProyectoAvance(int proyectoId)
        {
            IEnumerable<ProyectoAvance>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoAvance/{proyectoId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoAvance>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoAvance>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoAvance: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoAvance>> GetProyectoAvance(int proyectoId, int tareaId)
        {
            IEnumerable<ProyectoAvance>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoAvance/{proyectoId}/{tareaId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoAvance>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoAvance>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoAvance: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<ProyectoAvance> GetProyectoAvance(int proyectoId, int tareaId, int itemId)
        {
            ProyectoAvance? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoAvance/{proyectoId}/{tareaId}/{itemId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<ProyectoAvance>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoAvance: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoConsultor>> GetProyectoConsultor()
        {
            IEnumerable<ProyectoConsultor>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoConsultor/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoConsultor>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoConsultor>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoConsultor>> GetProyectoConsultor(int id)
        {
            IEnumerable<ProyectoConsultor>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoConsultor/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoConsultor>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoConsultor>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<ProyectoConsultor> GetProyectoConsultor(int id, int consultorId)
        {
            ProyectoConsultor? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoConsultor/{id}/{consultorId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<ProyectoConsultor>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoConsultor: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDetalleDocAdjunto>> GetProyectoDetalleDocAdjunto()
        {
            IEnumerable<ProyectoDetalleDocAdjunto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDetalleDocAdjunto/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDetalleDocAdjunto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDetalleDocAdjunto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDetalleDocAdjunto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDetalleDocAdjunto>> GetProyectoDetalleDocAdjunto(int id)
        {
            IEnumerable<ProyectoDetalleDocAdjunto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDetalleDocAdjunto/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDetalleDocAdjunto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDetalleDocAdjunto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDetalleDocAdjunto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDetalleDocAdjunto>> GetProyectoDetalleDocAdjunto(int id, int tareaId)
        {
            IEnumerable<ProyectoDetalleDocAdjunto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDetalleDocAdjunto/{id}/{tareaId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDetalleDocAdjunto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDetalleDocAdjunto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDetalleDocAdjunto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDetalleDocAdjunto>> GetProyectoDetalleDocAdjunto(int id, int tareaId, int itemId)
        {
            IEnumerable<ProyectoDetalleDocAdjunto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDetalleDocAdjunto/{id}/{tareaId}/{itemId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDetalleDocAdjunto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDetalleDocAdjunto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDetalleDocAdjunto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDetalle>> GetProyectoDetalle()
        {
            IEnumerable<ProyectoDetalle>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDetalle/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDetalle>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDetalle>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDetalle: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDetalle>> GetProyectoDetalle(int id)
        {
            IEnumerable<ProyectoDetalle>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDetalle/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDetalle>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDetalle>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDetalle: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDetalle>> GetProyectoDetalle(int id, int tareaId)
        {
            IEnumerable<ProyectoDetalle>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDetalle/{id}/{tareaId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDetalle>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDetalle>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDetalle: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<ProyectoDetalle> GetProyectoDetalle(int id, int tareaId, int item)
        {
            ProyectoDetalle? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDetalle/{id}/{tareaId}/{item}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<ProyectoDetalle>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDetalle: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDocAdjunto>> GetProyectoDocAdjunto()
        {
            IEnumerable<ProyectoDocAdjunto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDocAdjunto/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDocAdjunto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDocAdjunto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDocAdjunto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ProyectoDocAdjunto>> GetProyectoDocAdjunto(int id)
        {
            IEnumerable<ProyectoDocAdjunto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDocAdjunto/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ProyectoDocAdjunto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ProyectoDocAdjunto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDocAdjunto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<ProyectoDocAdjunto> GetProyectoDocAdjunto(int id, int docId)
        {
            ProyectoDocAdjunto? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ProyectoDocAdjunto/{id}/{docId}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<ProyectoDocAdjunto>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetProyectoDocAdjunto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }
    }
}
