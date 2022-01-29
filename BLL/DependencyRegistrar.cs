using BLL.Abstractions.Interfaces;
using BLL.Abstractions.Interfaces.Validators;
using BLL.Services;
using BLL.Validators;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class DependencyRegistrar
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddTransient<Room>();
            services.AddScoped<IRoomUserJointService, RoomUserJointService>();
            services.AddScoped<IRoomRoleJointService, RoomRoleJointService>();
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            DAL.DependencyRegistrar.ConfigureServices(services);
        }
    }
}