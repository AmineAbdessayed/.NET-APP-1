using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiProject.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiProject.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        }

        public string CreateToken(AppUser user)
        {
            //lclaims bech netehakmo fel user mteena chnowa ynajm yaaml wchnowa le 
                
                var claims= new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.GivenName,user.UserName)

                };
                //bech nekhtaro l type mtaa l encryption mteena
                var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor= new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires=DateTime.Now.AddDays(7),
                    SigningCredentials=creds,
                    Issuer=_configuration["JWT:Issuer"],
                    Audience=_configuration["JWT:Audience"]
                };
                var tokenHandler= new JwtSecurityTokenHandler();
                var token =tokenHandler.CreateToken(tokenDescriptor);

//token handler yrajaahelna string
                return tokenHandler.WriteToken(token);
                        }
    }
}