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
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}