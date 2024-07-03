using Microsoft.AspNetCore.Identity;

namespace Dll.Entity.IdentityEntity
{
    public static class StudentIdenditySeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new ApplicationUser
                {
                    Email = "MohamedReda243@gmail.com",
                    Name = "Mohamed Reda",
                    UserName = "MohamedReda243"
                };
                await userManager.CreateAsync(User, "Pa$$w0rd");
            }


        }
    }
}

