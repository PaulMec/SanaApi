using DB.Context;
using Microsoft.EntityFrameworkCore;
using SanaStoreAPI.Application.Services;
using SanaStoreAPI.Application.Services.Interfaces;
using SanaStoreAPI.Infrastructure.Data.Interfaces;
using SanaStoreAPI.Infrastructure.Data;
using SanaStoreAPI.Infrastructure.Services;
using SanaStoreAPI.Infrastructure.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configuración del Swagger y servicios API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración del Middleware de Sesión
builder.Services.AddDistributedMemoryCache();  // Usa un caché en memoria para almacenar las sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de vida de la sesión
    options.Cookie.HttpOnly = true; // Seguridad de la cookie
    options.Cookie.IsEssential = true; // Hace la cookie esencial para el funcionamiento de la app
});

// Acceso al contexto HTTP
builder.Services.AddHttpContextAccessor();

// Registro de repositorios
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

// Registro de servicios
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Configuración del controlador
builder.Services.AddControllers();

// Configuración de la base de datos
builder.Services.AddDbContext<SanaStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SanaConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin() // Permite solicitudes de cualquier origen
                          .AllowAnyMethod()  // Permite cualquier método HTTP
                          .AllowAnyHeader()); // Permite cualquier encabezado
});

var app = builder.Build();

/*
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SanaStoreContext>();
    context.Database.Migrate();
}
*/
// Aplicación del middleware de sesión
app.UseSession();

// Otras configuraciones de middleware
app.UseHttpsRedirection();

// Usar CORS
app.UseCors("CorsPolicy");
// Otras configuraciones de middleware
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
// Configuración del enrutamiento y middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SanaStoreAPI v1");
    });
}
app.Run();
