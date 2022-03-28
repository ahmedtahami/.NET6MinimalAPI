using Microsoft.IdentityModel.Tokens;

using MinimalAPI.DTOs;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalAPI.DTOs
{
    public record UserDto(string UserName, string Password);
    public record UserModel 
    { 
        [Required] 
        public string UserName { get; set; } 
        [Required] 
        public string Password { get; set; } 
    }
}

namespace MinimalAPI.TokenService
{
    public interface ITokenService 
    {
        string BuildToken(string key, string issuer, string audience, UserDto user); 
    }
    public class TokenService : ITokenService 
    { 
        private TimeSpan ExpiryDuration = new TimeSpan(0, 30, 0);
        public string BuildToken(string key, string issuer, string audience, UserDto user)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) }; 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)); 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature); 
            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials); 
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor); 
        }
    }
}