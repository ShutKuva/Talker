using BLL.Abstractions.Interfaces;
using BLL.Abstractions.Interfaces.Validators;
using BLL.Services;
using BLL.Validators;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configurations)
        {
            services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));
            services.AddTransient<Room>();
            services.AddScoped<IRoomUserJointService, RoomUserJointService>();
            services.AddScoped<IRoomRoleJointService, RoomRoleJointService>();
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            services.AddScoped<IHashHandler, HashHandler>();
            DAL.DependencyRegistrar.ConfigureServices(services, configurations);
        }
    }
}