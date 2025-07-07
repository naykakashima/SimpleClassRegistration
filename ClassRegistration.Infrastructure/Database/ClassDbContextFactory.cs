using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClassRegistration.Infrastructure.Database
{
    public class ClassDbContextFactory : IDesignTimeDbContextFactory<ClassDbContext>
    {
        public ClassDbContext CreateDbContext(string[] args)
        {
            var basePath = "C:\\Users\\5015940177\\Source\\Repos\\SimpleClassRegistration\\ClassRegistration.Presentation";

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // required to resolve file path correctly
            .AddJsonFile("appsettings.json") // read the config file
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

            var builder = new DbContextOptionsBuilder<ClassDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new ClassDbContext(builder.Options);
        }
    }
}
