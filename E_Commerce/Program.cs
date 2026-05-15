// Program.cs
using E_Commerce.Extensions;
using E_Commerce.Middlewares;
using E_Commerece.Core.Models.Identity;
using E_Commerece.Repository;
using E_Commerece.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services Configuration
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();           
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddCors(options=> {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontUrl"]);
                });
                
                
                });

            #endregion

            var app = builder.Build();

            #region Database Migration
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var usermanager = services.GetRequiredService<UserManager<AppUser>>();

            try
            {
                var context = services.GetRequiredService<StoreContext>();
                StoreContextSeed.SeedAsync(context);
                await context.Database.MigrateAsync();
                var identity =services.GetRequiredService<AppIdentityDbContext>();
                await identity.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }
            #endregion

            #region Middleware Pipeline
            app.UseSwaggerMiddleware();                   
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}