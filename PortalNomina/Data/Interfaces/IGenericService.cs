

using PortalNominaLibs.Models.Response;

namespace PortalNomina.Data.Interfaces
{
    public interface IGenericService
    {
        /// <summary>
		/// Post: Método para crear o actualizar registros en objetos de la base de datos a traves del método Post
		/// </summary>
		/// <returns>Objeto de la clase EventResponse </returns>
		/// <param name="methodName">Nombre del método de la Api a Ejecutar</param>
		/// <param name="model">Objeto de la clase a enviar en el cuerpo del mensaje</param>
        public Task<EventResponse> Post(string methodName, object model);

        /// <summary>
		/// Put: Método para actualizar registros en objetos de la base de datos a traves del método Put
		/// </summary>
		/// <returns>Objeto de la clase EventResponse </returns>
		/// <param name="methodName">Nombre del método de la Api a Ejecutar</param>
		/// <param name="model">Objeto de la clase a enviar en el cuerpo del mensaje</param>
		public Task<EventResponse> Put(string methodName, object model);

        /// <summary>
		/// Delete:  Metodo generico para borrado de datos de las tablas
		/// </summary>
		/// <param name="apiName"></param>
		/// <param name="id"></param>
		/// <returns>EventResponse</returns>
		public Task<EventResponse> Delete(string methodName, string id);

        /// <summary>
        /// Get: obtener datos de una api especifica a traves del metodo indicado como parámetro
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> Get(string method);



    }
}
