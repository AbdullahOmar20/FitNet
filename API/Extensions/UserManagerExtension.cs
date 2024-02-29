
using System.Security.Claims;

using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtension
    {
        //this gives user with thier address (Eager loading)
        public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager,
         ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(x=>x.Address)
                .SingleOrDefaultAsync(x=>x.Email == email);
            
        }

        //getting indivdual user and make our task to get the email claim much easier
        public static async Task<AppUser> FindByEmailFromClaimPrinciple(this UserManager<AppUser> userManager,
         ClaimsPrincipal user)
        {
            return await userManager.Users.SingleOrDefaultAsync(x=>x.Email ==
             user.FindFirstValue(ClaimTypes.Email));
        }
    }
}