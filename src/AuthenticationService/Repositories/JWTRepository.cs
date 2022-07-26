using Demo.AuthService.Infrastructure;
using Demo.AuthService.Models;
using Demo.AuthService.Repositories.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.AuthService.Repositories
{
    public class JWTRepository : IJWTRepository
    {
        private readonly IConfiguration _configuration;

        Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            { "user1","password1"},
            { "user2","password2"},
            { "user3","password3"},
        };

        public JWTRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Tokens Authenticate(User user)
        {
            if (!UserRecords.Any(x => x.Key == user.Name && x.Value == user.Password))
            {
                throw new UserNotFoundException(user.Name);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            if (!int.TryParse(_configuration["JWT:AccessTokenExpTimeInMins"], out int expirationTime))
            {
                throw new Exception("Cannot find or parse [AccessTokenExpTimeInMins] parameter");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                }),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
