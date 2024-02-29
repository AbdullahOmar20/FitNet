using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedusersAsync(UserManager<AppUser> userManager )
        {
            if(!userManager.Users.Any()){
                var user =new AppUser{
                    DisplayName="Abdullah",
                    Email = "abdullah@test.com",
                    UserName = "abdullah@test.com",
                    Address = new Address
                    {
                        FirstName = "abdullah",
                        LastName = "amer",
                        Street = "the street",
                        City = "tanta",
                        State = "gharbia",
                        ZipCode = "12345"
                    }
                };
                await userManager.CreateAsync(user, "@@sW0rd1234");
            }
        }
    }
}