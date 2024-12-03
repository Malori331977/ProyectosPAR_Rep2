
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

        public async Task<IEnumerable<CentroCosto>> GetCentroCosto()
        {
            IEnumerable<CentroCosto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/CentroCosto/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<CentroCosto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<CentroCosto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetCentroCosto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<CentroCosto> GetCentroCosto(string id)
        {
            CentroCosto? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/CentroCosto/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<CentroCosto>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetCentroCosto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Concepto>> GetConcepto()
        {
            IEnumerable<Concepto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Concepto/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Concepto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Concepto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetConcepto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Concepto> GetConcepto(string id)
        {
            Concepto? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Concepto/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Concepto>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetConcepto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<ConceptoFormula>> GetConceptoFormula()
        {
            IEnumerable<ConceptoFormula>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ConceptoFormula/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<ConceptoFormula>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<ConceptoFormula>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetConceptoFormula: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<ConceptoFormula> GetConceptoFormula(string id)
        {
            ConceptoFormula? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/ConceptoFormula/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<ConceptoFormula>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetConceptoFormula: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Departamento>> GetDepartamento()
        {
            IEnumerable<Departamento>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Departamento/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Departamento>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Departamento>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetDepartamento: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Departamento> GetDepartamento(string id)
        {
            Departamento? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Departamento/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Departamento>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetDepartamento: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<EntidadFinanciera>> GetEntidadFinanciera()
        {
            IEnumerable<EntidadFinanciera>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/EntidadFinanciera/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<EntidadFinanciera>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<EntidadFinanciera>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetEntidadFinanciera: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<EntidadFinanciera> GetEntidadFinanciera(string id)
        {
            EntidadFinanciera? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/EntidadFinanciera/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<EntidadFinanciera>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetEntidadFinanciera: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<EstadoEmpleado>> GetEstadoEmpleado()
        {
            IEnumerable<EstadoEmpleado>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/EstadoEmpleado/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<EstadoEmpleado>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<EstadoEmpleado>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetEstadoEmpleado: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<EstadoEmpleado> GetEstadoEmpleado(string id)
        {
            EstadoEmpleado? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/EstadoEmpleado/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<EstadoEmpleado>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetEstadoEmpleado: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Moneda>> GetMoneda()
        {
            IEnumerable<Moneda>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Moneda/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Moneda>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Moneda>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetMoneda: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Moneda> GetMoneda(string id)
        {
            Moneda? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Moneda/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Moneda>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetMoneda: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<Puesto>> GetPuesto()
        {
            IEnumerable<Puesto>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Puesto/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<Puesto>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<Puesto>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetPuesto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<Puesto> GetPuesto(string id)
        {
            Puesto? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/Puesto/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<Puesto>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetPuesto: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<TipoAusencia>> GetTipoAusencia()
        {
            IEnumerable<TipoAusencia>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoAusencia/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<TipoAusencia>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<TipoAusencia>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoAusencia: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<TipoAusencia> GetTipoAusencia(string id)
        {
            TipoAusencia? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoAusencia/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<TipoAusencia>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoAusencia: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<TipoCambio>> GetTipoCambio()
        {
            IEnumerable<TipoCambio>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoCambio/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<TipoCambio>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<TipoCambio>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoCambio: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<TipoCambio> GetTipoCambio(string id)
        {
            TipoCambio? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoCambio/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<TipoCambio>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoCambio: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<TipoCambioHist>> GetTipoCambioHist()
        {
            IEnumerable<TipoCambioHist>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoCambioHist/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<TipoCambioHist>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<TipoCambioHist>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoCambioHist: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<TipoCambioHist> GetTipoCambioHist(string id)
        {
            TipoCambioHist? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoCambioHist/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<TipoCambioHist>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoCambioHist: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<IEnumerable<TipoIncapacidad>> GetTipoIncapacidad()
        {
            IEnumerable<TipoIncapacidad>? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoIncapacidad/";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<TipoIncapacidad>>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                model = Enumerable.Empty<TipoIncapacidad>();
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoIncapacidad: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }

        public async Task<TipoIncapacidad> GetTipoIncapacidad(string id)
        {
            TipoIncapacidad? model = null;
            HttpResponseMessage? result = null;
            try
            {
                var method = $"/TipoIncapacidad/{id}";
                result = await _genericService.Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<TipoIncapacidad>();
                    readJob.Wait();
                    model = readJob.Result;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetTipoIncapacidad: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }
            return model!;
        }
    }
}
