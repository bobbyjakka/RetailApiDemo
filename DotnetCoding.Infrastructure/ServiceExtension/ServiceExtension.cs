using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Infrastructure.Repositories;
using System.IO;

namespace DotnetCoding.Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {

        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Entity Framework Core with the connection string from appsettings.json
            services.AddDbContext<DbContextClass>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IApprovalQueueRepository, ApprovalQueueRepository>();

            return services;
        }
    }
}
