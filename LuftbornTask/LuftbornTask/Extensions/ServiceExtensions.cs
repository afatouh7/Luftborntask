using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LuftbornTask.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        public static void ConfigureUnitOfWorkManager(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
    }
}