using Microsoft.AspNetCore.Identity;
namespace SchoolTransport.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public string TenantId { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}