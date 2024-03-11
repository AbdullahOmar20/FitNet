

using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static string RetrieveEmail(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}