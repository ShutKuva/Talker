using DAL.Abstractions.Interfaces;
using DAL.EFContext;
using DAL.EFRepository;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISerializer, JsonSerializer>();
            services.AddScoped<TalkerDbContext>();
        }
    }
}