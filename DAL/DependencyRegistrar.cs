using Core;
using DAL.Abstractions.Interfaces;
using DAL.EFContext;
using DAL.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DAL
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configurations)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISerializer, JsonSerializer>();
            string temp = configurations.GetSection("SQLDBConnection")
                .GetChildren()
                .Where(section => section.Key == "ConnectionString")
                .FirstOrDefault().Value;
            services.AddDbContext<TalkerDbContext>(options => options
                .UseSqlServer(configurations.GetSection("SQLDBConnection")
                .GetChildren()
                .Where(section => section.Key == "ConnectionString")
                .FirstOrDefault().Value));
        }
    }
}