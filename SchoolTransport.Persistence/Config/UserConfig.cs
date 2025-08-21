using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolTransport.Domain.Entities.Identity;

namespace SchoolTransport.Persistence.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var passwordHasher = new PasswordHasher<User>();
            builder.HasData(
                new User
                {
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@schooltransport.com",
                    NormalizedEmail = "ADMIN@SCHOOLTRANSPORT.COM",
                    EmailConfirmed = true,
                    FirstName = "System",
                    LastName = "Administrator",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    TenantId = "1",
                    PasswordHash = passwordHasher.HashPassword(new User(), "Admin123!")
                },
                new User
                {
                    Id = "2",
                    UserName = "testuser",
                    NormalizedUserName = "TESTUSER",
                    Email = "test@schooltransport.com",
                    NormalizedEmail = "TEST@SCHOOLTRANSPORT.COM",
                    EmailConfirmed = true,
                    FirstName = "Test",
                    LastName = "User",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    TenantId = "2",
                    PasswordHash = passwordHasher.HashPassword(new User(), "Test123!")
                }
            );
        }
    }
}