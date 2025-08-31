using ejemplo_api_rest.Repository;
using ejemplo_api_rest.Services;

namespace ejemplo_api_rest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Registrar los servicios y repositorios como singleton
            builder.Services.AddSingleton<IProductoRepository, ProductoRepository>();
            builder.Services.AddSingleton<IProductoService, ProductoService>();

            // Configuración de OpenAPI / Swagger
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
