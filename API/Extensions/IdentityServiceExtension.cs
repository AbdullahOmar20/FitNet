
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace API.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<AppIdentityDbContext>(opt=>
                {
                    opt.UseSqlite(config.GetConnectionString("IdentityConnection"));
                }
            );
            service.AddIdentityCore<AppUser>(opt=>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequiredUniqueChars = 1;
            }
            ).AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();
            //identifing the authentication scheme
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(opt=>
            {
                //what do we need to validate the token
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    //what we will validate aganist

                    ValidateIssuerSigningKey = true,
                    //making sure what does it checking aganist
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                    //checking we have valide issuer
                    ValidIssuer = config["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false 
                };
            }
            );


            service.AddAuthorization();
            return service;
        }
    }
}