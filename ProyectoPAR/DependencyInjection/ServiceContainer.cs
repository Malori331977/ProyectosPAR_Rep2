

using CurrieTechnologies.Razor.SweetAlert2;
using ProyectoPAR.Data;
using ProyectoPAR.Data.Interfaces;

namespace ProyectoPAR.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
            Services.AddScoped<IGenericService, GenericService>();
            Services.AddScoped<IConfigurationService, ConfigurationService>();
            Services.AddScoped<IMantenimientoService, MantenimientoService>();
            Services.AddScoped<IGridExtensions, GridExtensions>();
            Services.AddScoped<LoginState>();
            Services.AddScoped<IEmailRepository, EmailRepository>();
            Services.AddScoped<IProcesoService, ProcesoService>();
            Services.AddScoped<ExportData>();
            
            Services.AddSweetAlert2();
            Services.AddBlazorBootstrap();



            return Services;

        }
    }
}
