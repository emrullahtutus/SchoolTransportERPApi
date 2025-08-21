using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Token;
using SchoolTransport.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Concrete
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(int second, User user)
        {
            Token token = new();
            var now = DateTime.UtcNow;
            Console.WriteLine($"Current UTC Time: {now}");

            // Güvenlik anahtarını UTF8 byte dizisine çevirip simetrik anahtar oluşturuyoruz
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = now.AddSeconds(second);
            Console.WriteLine($"Token Expiration: {token.Expiration}");
            Console.WriteLine($"Seconds added: {second}");

            // Claims listesi güncellendi
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email ?? ""),
                new("tenantId", user.TenantId ?? ""), 
                new("firstName", user.FirstName ?? ""),
                new("lastName", user.LastName ?? "")
            };

            JwtSecurityToken securityToken = new
            (
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: now,
                signingCredentials: signingCredentials,
                claims: claims // Claims listesi kullanıldı
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}