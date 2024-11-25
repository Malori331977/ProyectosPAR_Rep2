

using CurrieTechnologies.Razor.SweetAlert2;
using PortalNomina.Data;
using PortalNomina.Data.Interfaces;

namespace PortalNomina.DependencyInjection
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

            Services.AddSweetAlert2();
            Services.AddBlazorBootstrap();



            return Services;

        }
    }
}
