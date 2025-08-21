using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolTransport.Domain.Entities.Student;

namespace SchoolTransport.Persistence.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
                // Tenant 1 - Admin
                new Student { Id = 1, FullName = "Ayşe Yılmaz", PhoneNumber = "0532-123-4567", SchoolId = 1, Address = "Aydın Mahallesi, İzmir", Distance = 5, TenantId = "1" },
                new Student { Id = 2, FullName = "Fatma Kaya", PhoneNumber = "0533-234-5678", SchoolId = 1, Address = "Balçova Caddesi, İzmir", Distance = 3, TenantId = "1" },
                new Student { Id = 3, FullName = "Zeynep Demir", PhoneNumber = "0534-345-6789", SchoolId = 2, Address = "Konak Sokak, Ankara", Distance = 8, TenantId = "1" },
                new Student { Id = 4, FullName = "Elif Çelik", PhoneNumber = "0535-456-7890", SchoolId = 2, Address = "Çankaya Caddesi, Ankara", Distance = 1, TenantId = "1" },

                // Tenant 2 - Test User
                new Student { Id = 5, FullName = "Büşra Acar", PhoneNumber = "0536-567-8901", SchoolId = 3, Address = "Bostancı Mahallesi, İstanbul", Distance = 12, TenantId = "2" },
                new Student { Id = 6, FullName = "Merve Öztürk", PhoneNumber = "0537-678-9012", SchoolId = 3, Address = "Kadıköy Sokak, İstanbul", Distance = 7, TenantId = "2" },
                new Student { Id = 7, FullName = "Seda Arslan", PhoneNumber = "0538-789-0123", SchoolId = 4, Address = "Şirinyer Caddesi, İzmir", Distance = 4, TenantId = "2" },
                new Student { Id = 8, FullName = "Gamze Polat", PhoneNumber = "0539-890-1234", SchoolId = 4, Address = "Bornova Sokak, İzmir", Distance = 6, TenantId = "2" }
            );
        }
    }
}