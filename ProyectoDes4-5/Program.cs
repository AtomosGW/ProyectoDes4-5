using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.BD;
using ProyectoDes4_5.Repositorio;
using ProyectoDes4_5.Interfaz;
using ProyectoDes4_5.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "WEB" // Cambiar la carpeta de archivos estáticos a "WEB"
});

// Configurar servicios necesarios
builder.Services.AddControllers();  // Usamos AddControllers para una API sin vistas

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

// Configurar el contexto de la base de datos
builder.Services.AddDbContext<ConexionDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios genéricos y específicos
builder.Services.AddScoped(typeof(BaseService<>)); // Servicio genérico para CRUD
builder.Services.AddScoped<PedidoProductoService>(); // Servicio específico para PedidoProducto
builder.Services.AddScoped<IAsignacionesRepository, AsignacionesRepository>(); // Repositorio para Asignaciones

// Crear la aplicación
var app = builder.Build();

// Configuración de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // No necesitamos esto si vamos a manejar los errores directamente
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Sirve archivos desde la carpeta "WEB"
app.UseRouting();
app.UseCors("AllowAll");

// Configuración para capturar errores y devolver una respuesta adecuada en formato JSON
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500; // Internal Server Error
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception != null)
        {
            // Log del error
            // Aquí puedes usar un logger para registrar la excepción
            var result = new { message = exception.Message };
            await context.Response.WriteAsJsonAsync(result);  // Devolvemos el mensaje de error en JSON
        }
    });
});

app.MapControllers();
// Ejecutar la aplicación
app.Run();
