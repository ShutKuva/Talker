using System.IO;
using BLL;
using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbAutoCreator dbAutoCreator = new DbAutoCreator(@"C:\Users\mezik\source\repos\Talker\DB");
            dbAutoCreator.GenerateDb();
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<App>().StartApp();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<PasswordValidationParameters>(configuration.GetSection("PasswordValidationParameters"));
            
            services.AddScoped<App>();
            DependencyRegistrar.ConfigureServices(services);
        }
    }
}