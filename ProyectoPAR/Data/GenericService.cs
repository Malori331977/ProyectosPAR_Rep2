using ProyectoPAR.Data.Interfaces;
using ProyectoPAR.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ProyectoParLibs.Models.Request;
using ProyectoParLibs.Models.Response;

namespace ProyectoPAR.Data
{
    public class GenericService : IGenericService
    {
        private readonly ILogger<GenericService> _logger;
        private string InterfaceName = "GenericService";
        public GenericService(ILogger<GenericService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GetUrlApi: Método para obtener los datos de configuracion que se encuentran en el appsettings.json
        /// </summary>
        /// <returns>Instancia de la clase AppSettings</returns>
        private async Task<AppSettings> GetUrlApi()
        {
            AppSettings? appSettings = null;

            try
            {
                // Build a config object, using env vars and JSON providers.
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var section = config.GetSection("AppSettings");

                appSettings = new AppSettings
                {

                    ApiPortalUrl = section.GetSection("ApiPortalUrl").Value!,
                    ApiPortalDataBase = section.GetSection("ApiPortalDataBase").Value!,
                    ApiPortalSchema = section.GetSection("ApiPortalSchema").Value!,
                };

            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetUrlApi: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }


            return appSettings!;
        }

        /// <summary>
        /// getToken: Método para obtener token de acceso a la Api
        /// </summary>
        /// <returns>String con el token solicitado</returns>

        private async Task<string> GetToken()
        {

            string? token = "";
            try
            {
                AppSettings appSettings = await GetUrlApi();
                using var client = new HttpClient();
                var api = appSettings.ApiPortalUrl + "/User";
                client.BaseAddress = new Uri(appSettings.ApiPortalUrl);
                var userRequest = new UserRequest
                {
                    ClientId = "197ac2e4bd0843c3974725a6544e1089c4a7dcae59087543ba6428c9914c35d9",
                    User = "GSITCR",
                    Password = "c5bbf3d10de5c6dfdad016e6e948a27d343b5e22f35471324388460c4e14a27c",
                    Schema = appSettings.ApiPortalSchema,
                    BDName = appSettings.ApiPortalDataBase
                };

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var postData = JsonSerializer.Serialize(userRequest);
                var contentData = new StringContent(postData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(api, contentData);

                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<UserResponse>();

                    if (respuesta is not null)
                        token = respuesta.Token;
                }
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.GetToken: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
            }

            return token ?? "";
        }

        /// <summary>
        /// Post: Método para crear o actualizar registros en objetos de la base de datos a traves del método Post
        /// </summary>
        /// <returns>Objeto de la clase EventResponse </returns>
        /// <param name="methodName">Nombre del método de la Api a Ejecutar</param>
        /// <param name="model">Objeto de la clase a enviar en el cuerpo del mensaje</param>
        public async Task<EventResponse> Post(string methodName, object model)
        {
            EventResponse? eventoPost = new();

            try
            {
                AppSettings appSettings = await GetUrlApi();
                methodName = appSettings.ApiPortalUrl + "/" + methodName;
                using var client = new HttpClient();
                client.BaseAddress = new Uri(appSettings.ApiPortalUrl);
                var token = await GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var postData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(postData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(methodName, contentData);

                eventoPost = await response.Content.ReadFromJsonAsync<EventResponse>();
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.Post{methodName}: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
                throw;
            }
            return eventoPost!;
        }

        /// <summary>
        /// Put: Método para actualizar registros en objetos de la base de datos a traves del método Put
        /// </summary>
        /// <returns>Objeto de la clase EventResponse </returns>
        /// <param name="methodName">Nombre del método de la Api a Ejecutar</param>
        /// <param name="model">Objeto de la clase a enviar en el cuerpo del mensaje</param>
        public async Task<EventResponse> Put(string methodName, object model)
        {
            EventResponse? eventoPut = new();

            try
            {
                AppSettings appSettings = await GetUrlApi();
                methodName = appSettings.ApiPortalUrl + "/" + methodName;
                using var client = new HttpClient();
                client.BaseAddress = new Uri(appSettings.ApiPortalUrl);
                var token = await GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var putData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(putData, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(methodName, contentData);

                eventoPut = await response.Content.ReadFromJsonAsync<EventResponse>();
            }
            catch (Exception e)
            {
                string error = e.InnerException is null ? e.Message : e.InnerException.Message;
                _logger.LogError($"{InterfaceName}.Put{methodName}: Se ha presentado un error al ejecutar el proceso. Detalle de Error: {error}");
                throw;
            }
            return eventoPut!;
        }

        /// <summary>
        /// Delete:  Metodo generico para borrado de datos de las tablas
        /// </summary>
        /// <param name="apiName"></param>
        /// <param name="id"></param>
        /// <returns>EventResponse</returns>
        public async Task<EventResponse> Delete(string methodName, string id)
        {
            EventResponse? respuesta = new();
            try
            {
                AppSettings appSettings = await GetUrlApi();
                methodName = $"{appSettings.ApiPortalUrl}/{methodName}/{id}";
                using var client = new HttpClient();
                client.BaseAddress = new Uri(appSettings.ApiPortalUrl);
                var token = await GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var responseTask = await client.DeleteAsync(methodName);

                respuesta = await responseTask.Content.ReadFromJsonAsync<EventResponse>();

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                throw;
            }
            return respuesta!;

        }

        public async Task<HttpResponseMessage> Get(string method)
        {
            AppSettings appSettings = await GetUrlApi();
            using var client = new HttpClient();
            client.BaseAddress = new Uri(appSettings.ApiPortalUrl);
            var api = $"{appSettings.ApiPortalUrl}{method}";
            var token = await GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await client.GetAsync(api);
        }




    }
}
