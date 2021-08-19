using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Api.Entity;
using WebApi.Api.ParamModel;

namespace WebApi.Api.Utils
{
    public class TokenHelper
    {
        public static string GenerateToekn(TokenParameter tokenParameter,Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameter.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(tokenParameter.Issuer, null, claims, expires: DateTime.UtcNow.AddMinutes(tokenParameter.AccessExpiration), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }
    }
}