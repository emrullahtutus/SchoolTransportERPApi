using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolTransport.Domain.Entities.Driver;

namespace SchoolTransport.Persistence.Config
{
    public class DriverConfig : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasData(
                // Tenant 1 - Admin
                new Driver { Id = 1, Name = "Ahmet Yılmaz", TenantId = "1" },
                new Driver { Id = 2, Name = "Mehmet Kaya", TenantId = "1" },
                new Driver { Id = 3, Name = "Ali Demir", TenantId = "1" },

                // Tenant 2 - Test User
                new Driver { Id = 4, Name = "Hasan Çelik", TenantId = "2" },
                new Driver { Id = 5, Name = "İbrahim Acar", TenantId = "2" }
            );
        }
    }
}