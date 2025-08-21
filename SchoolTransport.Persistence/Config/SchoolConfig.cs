using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolTransport.Domain.Entities.School;

namespace SchoolTransport.Persistence.Config
{
    public class SchoolConfig : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasData(
                // Tenant 1 - Admin
                new School { Id = 1, Name = "Atatürk İlkokulu", TenantId = "1" },
                new School { Id = 2, Name = "Mehmet Akif Ersoy Ortaokulu", TenantId = "1" },

                // Tenant 2 - Test User
                new School { Id = 3, Name = "Konya Fen Lisesi", TenantId = "2" },
                new School { Id = 4, Name = "Meram Anadolu Lisesi", TenantId = "2" }
            );
        }
    }
}