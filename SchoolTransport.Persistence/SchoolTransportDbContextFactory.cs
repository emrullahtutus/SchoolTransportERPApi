using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using SchoolTransport.Persistence.Context;

namespace SchoolTransport.Persistence.Context
{
    public class SchoolTransportDbContextFactory : IDesignTimeDbContextFactory<SchoolTransportDbContext>
    {
        public SchoolTransportDbContext CreateDbContext(string[] args)
        {
 
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "SchoolTransport.API"))
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("sqlConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("sqlConnection adında bir bağlantı dizesi bulunamadı. Lütfen SchoolTransport.API projesindeki appsettings.json dosyasını kontrol edin.");
            }
            var optionsBuilder = new DbContextOptionsBuilder<SchoolTransportDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new SchoolTransportDbContext(optionsBuilder.Options);
        }
    }
}