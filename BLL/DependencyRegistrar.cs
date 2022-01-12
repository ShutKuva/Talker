using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, CrudService>();
            services.AddScoped<IRoomService, RoomService>();
            DAL.DependencyRegistrar.ConfigureServices(services);
        }
    }
}