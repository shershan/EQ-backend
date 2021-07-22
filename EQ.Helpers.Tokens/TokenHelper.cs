using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EQ.Helpers.Tokens
{
    public static class TokenHelper
    {
        public static string CreateToken(string signInKey, string issuer, string audience, string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer,
              audience,
              expires: DateTime.Now.AddHours(16),
              signingCredentials: creds,
              claims: new List<Claim>()
              {
                  new Claim(ClaimTypes.Email, email),
                  new Claim(ClaimTypes.Role, role)
              });

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
