using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ToDoList.RestfulAPI.Interfaces;

namespace ToDoList.RestfulAPI.Models
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string key;

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string emailAddress, string password, List<User> users)
        {
            var user = users.FirstOrDefault(x => x.EmailAddress == emailAddress);
            bool isValidPassword = false;
            if (user != null)
            {
                isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
            }
            if(!users.Any(u => u.EmailAddress == emailAddress && isValidPassword))
            {
                return null;
            }

            var loggedUser = users.First(user => user.EmailAddress == emailAddress);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, emailAddress),
                    new Claim("role", loggedUser.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
