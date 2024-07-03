using Microsoft.AspNetCore.Identity;

namespace Dll.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }

        public string Name { get; set; }

        public string? StudentId { get; set; }

    }

}
