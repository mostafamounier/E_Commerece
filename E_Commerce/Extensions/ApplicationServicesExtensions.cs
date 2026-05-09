// ApplicationServicesExtensions.cs
using AutoMapper;
using E_Commerce.Errors;
using E_Commerce.Helpers;
using E_Commerece.Core;
using E_Commerece.Core.Repositories;
using E_Commerece.Repository;
using E_Commerece.Repository.Data;
using E_Commerece.Repository.Repositoriees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace E_Commerce.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfiles));

            // API Behavior Options - Validation Error Handling
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var validationErrorResponse = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            // Database Context
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UserIdentityConnection"));
            });
            
            services.AddSingleton<IConnectionMultiplexer>(Options => { 
                    
                var Connection = configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(Connection);
            });
            services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });


            return services;
        }
    }
}