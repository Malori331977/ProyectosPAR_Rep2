

using ProyectoParLibs.Models.Procesos;

namespace ProyectoPAR.Data.Interfaces
{
    public interface IProcesoService
    {
        public Task<IEnumerable<Reporte>> GetReporte();
        public Task<Reporte> GetReporte(int id);
        public Task<IEnumerable<Reporte>> GetReporteConsultor(int id);
        public Task<IEnumerable<Reporte>> GetReporte(string estado, string fechaInicial, string fechaFinal);
        public Task<IEnumerable<Reporte>> GetReporte(string estado, string fechaInicial, string fechaFinal, bool facturar, bool facturado);

        /// <summary>
        /// GetAdendum: Obtiene todos los registros de la tabla Adendums
        /// </summary>
        /// <returns>Lista de registros de la tabla Adendums</returns>
        public Task<IEnumerable<Adendum>> GetAdendum();

        /// <summary>
        /// GetAdendum: Obtiene una lista de registros de la tabla Adendums segun el parametro de ID de Contrato indicado
        /// </summary>
        /// <param name="idContrato"></param>
        /// <returns>Lista de registros de la tabla Adendums para un contrato especifico</returns>
        public Task<IEnumerable<Adendum>> GetAdendum(int idContrato);

        /// <summary>
        /// GetAdendum: obtiene un registro especifico de la tabla Adendums para un contrato y id especificos
        /// </summary>
        /// <param name="idContrato">id de contrato</param>
        /// <param name="id">id de adendum</param>
        /// <returns>Un registro especifico de la tabla Adendums</returns>
        public Task<Adendum> GetAdendum(int idContrato, int id);

        /// <summary>
        /// GetAgendaConsultor: Obtiene todos los registros de la tabla AgendaConsultor
        /// </summary>
        /// <returns>Lista de registros de la tabla AgendaConsultor</returns>
        public Task<IEnumerable<AgendaConsultor>> GetAgendaConsultor();

        /// <summary>
        /// GetAgendaConsultor: Obtiene un registro de la tabla AgendaConsultor segun el parametro de ID indicado
        /// </summary>
        /// <param name="consultorId"></param>
        /// <returns>Lista de registros de la tabla AgendaConsultor para un id de agenda especifico</returns>
        public Task<AgendaConsultor> GetAgendaConsultor(int id);

        /// <summary>
        /// GetAgendaConsultorByProyecto: Obtiene una lista de registros de la tabla AgendaConsultor segun el parametro de ID de proyecto indicado
        /// </summary>
        /// <param name="proyectoId"></param>
        /// <returns>Lista de registros de la tabla AgendaConsultor para un proyecto especifico</returns>
        public Task<IEnumerable<AgendaConsultor>> GetAgendaConsultorByProyecto(int proyectoId);

        /// <summary>
        /// GetAgendaConsultorByConsultor: Obtiene una lista de registros de la tabla AgendaConsultor segun el parametro de ID de proyecto indicado
        /// </summary>
        /// <param name="consultorId"></param>
        /// <returns>Lista de registros de la tabla AgendaConsultor para un consultor especifico</returns>
        public Task<IEnumerable<AgendaConsultor>> GetAgendaConsultorByConsultor(int consultorId);

        /// <summary>
        /// GetAgendaConsultorByConsultor: Obtiene todos los registros de la tabla AgendaConsultor para un consultor especifico y un rango de fechas
        /// </summary>
        /// <param name="consultorId"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns>Lista de registros de la tabla AgendaConsultor</returns>
        public Task<IEnumerable<AgendaConsultor>> GetAgendaConsultorByConsultor(int consultorId, string fechaInicio, string fechaFinal);

        /// <summary>
        /// GetAgendaConsultorByConsultor: Obtiene el registro de la tabla AgendaConsultor para un consultor y ID especifico
        /// </summary>
        /// <param name="id"></param>
        /// <param name="consultorId"></param>
        /// <returns>Registros de la tabla AgendaConsultor</returns>
        public Task<AgendaConsultor> GetAgendaConsultorByConsultor(int id, int consultorId);

        /// <summary>
        /// GetContrato: Obtiene todos los registros de la tabla Contratos
        /// </summary>
        /// <returns>Lista de registros de la tabla Contratos</returns>
        public Task<IEnumerable<Contrato>> GetContrato();

        /// <summary>
        /// GetContrato: Obtiene una lista de registros de la tabla Contratos segun el parametro de ID Contrato indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un registro especifico de la tabla Contratos</returns>
        public Task<Contrato> GetContrato(int id);

        /// <summary>
        /// GetContratoByEstado: obtiene los registros de la tabla Contratos para una estado especifico
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public Task<IEnumerable<Contrato>> GetContratoByEstado(string estado);

        /// <summary>
        /// GetLicencia: Obtiene todos los registros de la tabla Licencias
        /// </summary>
        /// <returns>Lista de registros de la tabla Licencias</returns>
        public Task<IEnumerable<Licencia>> GetLicencia();

        /// <summary>
        /// GetLicencia: Obtiene una lista de registros de la tabla Licencias segun el parametro de ID Licencia indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un registro especifico de la tabla Licencias</returns>
        public Task<IEnumerable<Licencia>> GetLicencia(int productoId, int contratoId);

        /// <summary>
        /// GetLicencia: Obtiene un registro de la tabla Licencias segun el parametro de ID Licencia indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un registro especifico de la tabla Licencias</returns>
        public Task<Licencia> GetLicencia(int id);

        /// <summary>
        /// GetProyecto: Obtiene todos los registros de la tabla Proyectos
        /// </summary>
        /// <returns>Lista de registros de la tabla Proyectos</returns>
        public Task<IEnumerable<Proyecto>> GetProyecto();

        /// <summary>
        /// GetProyecto: Obtiene una lista de registros de la tabla Proyectos segun el parametro de ID proyecto indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un registro especifico de la tabla Proyectos</returns>
        public Task<Proyecto> GetProyecto(int id);

        /// <summary>
        /// GetProyectoByEstado: obtiene los registros de la tabla Proyectos para un estado especifico
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public Task<IEnumerable<Proyecto>> GetProyectoByEstado(string estado);

        /// <summary>
        /// GetProyectoByConsultor: obtiene los registros de la tabla Proyectos para un consultor especifico
        /// </summary>
        /// <param name="consultor"></param>
        /// <returns></returns>
        public Task<IEnumerable<Proyecto>> GetProyectoByConsultor(int consultor);

        /// <summary>
        /// GetProyectoAvance: Obtiene todos los registros de la tabla ProyectosAvances
        /// </summary>
        /// <returns>Lista de registros de la tabla ProyectoAvances</returns>
        public Task<IEnumerable<ProyectoAvance>> GetProyectoAvance();

        /// <summary>
        /// GetProyectoAvance: Obtiene lista de registros de la tabla ProyectosAvances segun el parametro de proyecto indicado indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un registro especifico de la tabla ProyectosAvances</returns>
        public Task<IEnumerable<ProyectoAvance>> GetProyectoAvance(int proyectoId);

        /// <summary>
        /// GetProyectoAvance: Obtiene lista de registros de la tabla ProyectosAvances segun el parametro de proyecto y tarea indicados 
        /// </summary>
        /// <param name="proyectoId"></param>
        /// <param name="tareaId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ProyectoAvance>> GetProyectoAvance(int proyectoId, int tareaId);

        // <summary>
        /// GetProyectoAvance: Obtiene un de registro de la tabla ProyectosAvances segun el parametro de proyecto, tarea e item indicados 
        /// </summary>
        /// <param name="proyectoId"></param>
        /// <param name="tareaId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ProyectoAvance>> GetProyectoAvance(int proyectoId, int tareaId, int itemId);

        /// <summary>
        /// GetProyectoConsultor: Obtiene todos los registros de la tabla ProyectosConsultores
        /// </summary>
        /// <returns>Lista de registros de la tabla ProyectosConsultores</returns>
        public Task<IEnumerable<ProyectoConsultor>> GetProyectoConsultor();

        /// <summary>
        /// GetProyectoConsultor: Obtiene un lista de registros de la tabla ProyectosConsultores segun el parametro de ID indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns> un lista de registros de la tabla ProyectosConsultores</returns>

        public Task<IEnumerable<ProyectoConsultor>> GetProyectoConsultor(int id);

        /// <summary>
        /// GetProyectoConsultor: Obtiene un registro de la tabla ProyectosConsultores segun el parametro de id proyecto y consultorid indicado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="consultorId"></param>
        /// <returns></returns>
        public Task<ProyectoConsultor> GetProyectoConsultor(int id, int consultorId);

        /// <summary>
        /// GetProyectoDetalleDocAdjunto: Obtiene todos los registros de la tabla ProyectosDetalleDocsAdjuntos
        /// </summary>
        /// <returns>Lista de registros de la tabla ProyectosDetalleDocsAdjuntos</returns>
        public Task<IEnumerable<ProyectoDetalleDocAdjunto>> GetProyectoDetalleDocAdjunto();

        /// <summary>
        /// GetProyectoDetalleDocAdjunto: Obtiene un lista de registros de la tabla ProyectosDetalleDocsAdjuntos segun el parametro de id Proyecto indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns> un lista de registros de la tabla ProyectosDetalleDocsAdjuntos</returns>

        public Task<IEnumerable<ProyectoDetalleDocAdjunto>> GetProyectoDetalleDocAdjunto(int id);

        /// <summary>
        /// GetProyectoDetalleDocAdjunto: Obtiene los registros de la tabla ProyectosDetalleDocsAdjuntos segun el parametro de id proyecto y tarea indicado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tareaId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ProyectoDetalleDocAdjunto>> GetProyectoDetalleDocAdjunto(int id, int tareaId);


        /// <summary>
        /// GetProyectoDetalleDocAdjunto:  Obtiene los registros de la tabla ProyectosDetalleDocsAdjuntos segun el parametro de id proyecto, tarea e item indicado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tareaId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Task<IEnumerable<ProyectoDetalleDocAdjunto>> GetProyectoDetalleDocAdjunto(int id, int tareaId, int itemId);

        /// <summary>
        /// GetProyectoDetalle: Obtiene todos los registros de la tabla ProyectosDetalle
        /// </summary>
        /// <returns>Lista de registros de la tabla ProyectosDetalle</returns>
        public Task<IEnumerable<ProyectoDetalle>> GetProyectoDetalle();

        /// <summary>
        /// GetProyectoDetalle: Obtiene un lista de registros de la tabla ProyectosDetalle segun el parametro de ID indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns> un lista de registros de la tabla ProyectosDetalle</returns>

        public Task<IEnumerable<ProyectoDetalle>> GetProyectoDetalle(int id);

        /// <summary>
        /// GetProyectoDetalle: Obtiene un lista de registros de la tabla ProyectosDetalle segun el parametro indicados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tareaId"></param>        
        /// <returns> un lista de registros de la tabla ProyectosDetalle</returns>
        public Task<IEnumerable<ProyectoDetalle>> GetProyectoDetalle(int id, int tareaId);


        /// <summary>
        /// GetProyectoDetalle: Obtiene un registro de la tabla ProyectosDetalle segun el parametro de id proyecto, tarea e item indicados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tareaId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<ProyectoDetalle> GetProyectoDetalle(int id, int tareaId, int item);


        /// <summary>
        /// GetProyectoDocAdjunto: Obtiene todos los registros de la tabla ProyectosDocsAdjuntos
        /// </summary>
        /// <returns>Lista de registros de la tabla ProyectosDocsAdjuntos</returns>
        public Task<IEnumerable<ProyectoDocAdjunto>> GetProyectoDocAdjunto();

        /// <summary>
        /// GetProyectoDocAdjunto: Obtiene un lista de registros de la tabla ProyectosDocsAdjuntos segun el parametro de ID indicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns> un lista de registros de la tabla ProyectosDocsAdjuntos</returns>

        public Task<IEnumerable<ProyectoDocAdjunto>> GetProyectoDocAdjunto(int id);

        /// <summary>
        /// GetProyectoDocAdjunto: Obtiene un registro de la tabla ProyectosDocsAdjuntos segun el parametro de id proyecto y consultorid indicado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        public Task<ProyectoDocAdjunto> GetProyectoDocAdjunto(int id, int docId);
    }
}
