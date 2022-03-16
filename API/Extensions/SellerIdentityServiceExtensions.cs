using System.Text;
using Core.Entities.SellerIdentity;
using Infrastructure.SellerIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class SellerIdentityServiceExtensions
    {
        public static IServiceCollection AddSellerIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            var builder = services.AddIdentityCore<AppSeller>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppSellerIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppSeller>>();

            services.AddAuthentication();

            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options =>
            //     {
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
            //             ValidIssuer = config["Token:Issuer"],
            //             ValidateIssuer = true,
            //             ValidateAudience = false
            //         };
            //     });

            return services;
        }
        
    }
}