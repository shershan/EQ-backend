using EQ.Constants;
using EQ.Models.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EQ.DAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EQContext>
    {
        public EQContext CreateDbContext(string[] args)
        {
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            var settings = new DbSettings();
            configuration.GetSection(ConfigurationConstants.DBSectionName).Bind(settings);

            var builder = new DbContextOptionsBuilder<EQContext>();
            builder.UseSqlServer(settings.DefaultConnection, b => b.MigrationsAssembly(settings.MigrationAsseblyName));

            return new EQContext(builder.Options);
        }
    }
}