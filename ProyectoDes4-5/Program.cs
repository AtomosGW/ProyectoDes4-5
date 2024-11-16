using ProyectoDes4_5.Interfaz;
using ProyectoDes4_5.Repositorio;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configurar los servicios necesarios
builder.Services.AddControllersWithViews();

// Configuraci�n de CORS (especificar el origen permitido o usar "AllowAnyOrigin" para permitir todos)
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

// Configurar el repositorio e inyecci�n de dependencias (ejemplo, se debe agregar AppDbContext y IAsignacionesRepository)
builder.Services.AddScoped<IAsignacionesRepository, AsignacionesRepository>();

// Configurar el contexto de la base de datos (Aseg�rate de configurar tu AppDbContext)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configurar Kestrel (si es necesario)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Configura el puerto espec�fico si es necesario (ejemplo 5001 para HTTPS)
    serverOptions.ListenAnyIP(5001);  // Por ejemplo, escucha en el puerto 5001
    serverOptions.ListenAnyIP(5000);  // Por ejemplo, escucha en el puerto 5000 para HTTP
});

// Agregar autenticaci�n y autorizaci�n (si es necesario)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
});

// Habilitar la autenticaci�n (si es necesario configurar JWT o autenticaci�n basada en cookies)
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer(options =>
{
    options.Authority = "https://localhost:5001"; // Configura la URL de la autoridad JWT
    options.Audience = "api"; // Configura el nombre del recurso
});

var app = builder.Build();

// Configurar la tuber�a de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar CORS para que se puedan realizar peticiones desde otros dominios si es necesario
app.UseCors("AllowAll");  // Utiliza la pol�tica de CORS configurada previamente

// Autenticaci�n y autorizaci�n (si es necesario)
app.UseAuthentication();
app.UseAuthorization();

// Mapear las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
