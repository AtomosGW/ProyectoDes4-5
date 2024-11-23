using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ProyectoDes4_5.BD;  // Asegúrate de tener el namespace correcto
using ProyectoDes4_5.Repositorio;
using ProyectoDes4_5.Interfaz;

// Configurar opciones al crear el builder
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "WEB" // Cambiar la carpeta de archivos estáticos a "WEB"
});

// Configurar los servicios necesarios
builder.Services.AddControllersWithViews();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configurar el repositorio e inyección de dependencias
builder.Services.AddScoped<IAsignacionesRepository, AsignacionesRepository>();

// Configurar el contexto de la base de datos (ConexionDB)
builder.Services.AddDbContext<ConexionDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configurar la tubería de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Sirve archivos desde la carpeta "WEB"
app.UseRouting();
app.UseCors("AllowAll");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Asignaciones}/{action=Index}/{id?}");

app.Run();
