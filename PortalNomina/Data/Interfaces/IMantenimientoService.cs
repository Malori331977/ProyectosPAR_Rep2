
using PortalNominaLibs.Models.Mantenimientos;

namespace PortalNomina.Data.Interfaces
{
    public interface IMantenimientoService
    {
        public Task<IEnumerable<Carrera>> GetCarrera();
        public Task<Carrera> GetCarrera(string id);
        public Task<IEnumerable<CentroCosto>> GetCentroCosto();
        public Task<CentroCosto> GetCentroCosto(string id);
        public Task<IEnumerable<Concepto>> GetConcepto();
        public Task<Concepto> GetConcepto(string id);
        public Task<IEnumerable<ConceptoFormula>> GetConceptoFormula();
        public Task<ConceptoFormula> GetConceptoFormula(string id);
        public Task<IEnumerable<Departamento>> GetDepartamento();
        public Task<Departamento> GetDepartamento(string id);
        public Task<IEnumerable<EntidadFinanciera>> GetEntidadFinanciera();
        public Task<EntidadFinanciera> GetEntidadFinanciera(string id);
        public Task<IEnumerable<EstadoEmpleado>> GetEstadoEmpleado();
        public Task<EstadoEmpleado> GetEstadoEmpleado(string id);
        public Task<IEnumerable<Moneda>> GetMoneda();
        public Task<Moneda> GetMoneda(string id);
        public Task<IEnumerable<Puesto>> GetPuesto();
        public Task<Puesto> GetPuesto(string id);
        public Task<IEnumerable<TipoAusencia>> GetTipoAusencia();
        public Task<TipoAusencia> GetTipoAusencia(string id);
        public Task<IEnumerable<TipoCambio>> GetTipoCambio();
        public Task<TipoCambio> GetTipoCambio(string id);
        public Task<IEnumerable<TipoCambioHist>> GetTipoCambioHist();
        public Task<TipoCambioHist> GetTipoCambioHist(string id);
        public Task<IEnumerable<TipoIncapacidad>> GetTipoIncapacidad();
        public Task<TipoIncapacidad> GetTipoIncapacidad(string id);
    }
}
