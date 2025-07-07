using ClassRegistration.Application.Interfaces;
using ClassRegistration.Application.Services;
using ClassRegistration.Infrastructure.Database;
using ClassRegistration.Infrastructure.Interfaces;
using ClassRegistration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ClassRegistration.ConsoleApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            using IHost host = CreateHostBuilder(args, configuration).Build();

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClassDbContext>();
                await dbContext.Database.MigrateAsync();
            }


            var menu = host.Services.GetRequiredService<Menu>();
            
            await menu.ShowMenu();
            await host.RunAsync();

            static IHostBuilder CreateHostBuilder(string[] args, IConfigurationRoot configuration) => Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
            {
                services.AddDbContext<ClassDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                services.AddScoped<IRegistrationService, RegistrationService>();
                services.AddScoped<IStudentRepository, StudentRepository>();
                services.AddScoped<IClassRepository, ClassRepository>();
                services.AddScoped<Menu>();
            });

            /**
             * Make sure to set appsetting.json to copy if new inside properties (vs)
            */



        }
    }
}