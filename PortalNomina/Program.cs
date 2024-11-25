using PortalNomina.DependencyInjection;
using PortalNomina.Models;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.GetSection("AppSettings");
var urlOrigen = settings.GetSection("UrlOrigen");
var withCors = settings.GetSection("WithCors");
var withHeadersMiddleware = settings.GetSection("WithHeadersMiddleware");


// Configure HSTS
// https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?WT.mc_id=DT-MVP-5003978#http-strict-transport-security-protocol-hsts
// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Strict-Transport-Security
if (withHeadersMiddleware.Value == "S")
{
    builder.Services.AddHsts(options =>
    {
        options.MaxAge = TimeSpan.FromDays(365);
        options.IncludeSubDomains = true;
        options.Preload = true;
    });
}

if (withHeadersMiddleware.Value == "S")
    // Configure HTTPS redirection
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
        options.HttpsPort = 443;
    });


if (withCors.Value == "S")
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder.WithOrigins(urlOrigen.Value!)
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowAnyHeader());
    });

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddApplicationService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    if (withHeadersMiddleware.Value == "S")
    {
        // Add other security headers
        app.UseMiddleware<SecurityHeadersMiddleware>();
    }
    app.UseHttpsRedirection();
}

if (withCors is not null)
    if (withCors.Value == "S")
        app.UseCors("CorsPolicy");


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
