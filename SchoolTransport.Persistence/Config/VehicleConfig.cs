using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolTransport.Domain.Entities.Vehicle;

namespace SchoolTransport.Persistence.Config
{
    public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasData(
                // Tenant 1 - Admin
                new Vehicle { Id = 1, PlateNumber = "42 ABC 123", DriverId = 1, TenantId = "1" },
                new Vehicle { Id = 2, PlateNumber = "42 DEF 456", DriverId = 2, TenantId = "1" },
                new Vehicle { Id = 3, PlateNumber = "42 GHI 789", DriverId = 3, TenantId = "1" },

                // Tenant 2 - Test User
                new Vehicle { Id = 4, PlateNumber = "42 JKL 012", DriverId = null, TenantId = "2" },
                new Vehicle { Id = 5, PlateNumber = "42 MNO 345", DriverId = 4, TenantId = "2" }
            );
        }
    }
}