

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities.SellerIdentity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class SellerTokenService : ISellerTokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public SellerTokenService(IConfiguration config)
        {
            _config = config;
             _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        public string CreateToken(AppSeller seller)
        {
            var claims =new List<Claim>
            {
                new Claim(ClaimTypes.Email,seller.Email),
                new Claim(ClaimTypes.GivenName,seller.SellerName)
            };
            var cred =new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials=cred,
                Issuer = _config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            
        }
    }
}