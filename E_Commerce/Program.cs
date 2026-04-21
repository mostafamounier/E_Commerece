// Program.cs
using E_Commerce.Extensions;
using E_Commerce.Middlewares;
using E_Commerece.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services Configuration
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();           // ✅ Swagger services
            builder.Services.AddApplicationServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            #region Database Migration
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }
            #endregion

            #region Middleware Pipeline
            app.UseSwaggerMiddleware();                      // ✅ Swagger pipeline
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}