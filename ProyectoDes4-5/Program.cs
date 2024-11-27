using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.Repositorio;
using ProyectoDes4_5.Interfaz;
using ProyectoDes4_5.Services;
using ProyectoDes4_5.Models;
using Microsoft.AspNetCore.Diagnostics;


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "WEB" // Cambiar la carpeta de archivos estáticos a "WEB"
});

// Configurar servicios necesarios para MVC (para vistas Razor)
builder.Services.AddControllersWithViews();  // Usa AddControllersWithViews para poder manejar vistas Razor

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración del contexto de base de datos
builder.Services.AddDbContext<WebPedidosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar otros servicios
builder.Services.AddScoped(typeof(BaseService<>)); // Servicio genérico para CRUD
builder.Services.AddScoped<PedidoProductoService>(); // Servicio específico
builder.Services.AddScoped<IAsignacionesRepository, AsignacionesRepository>(); // Repositorio

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles(); // Sirve archivos desde la carpeta "WEB"
app.UseRouting();
app.UseCors("AllowAll");

// Configurar el uso de vistas Razor
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Asignaciones}/{action=Index}/{id?}");

// Redirigir a la página principal si se accede a la raíz de la aplicación
app.MapGet("/", () => Results.Redirect("/api/asignaciones/index"));

// Captura de excepciones y manejo de errores
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception != null)
        {
            var result = new { message = exception.Message };
            await context.Response.WriteAsJsonAsync(result);
        }
    });
});

// Mapea controladores
app.MapControllers();

// Ejecuta la aplicación
app.Run();
