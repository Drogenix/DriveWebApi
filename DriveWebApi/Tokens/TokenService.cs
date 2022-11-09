using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DriverWebApi.Services.Tokens
{
    public class TokenService : ITokenService
    {

        public string CreateToken(string login, int id)
        {
            var claims = new List<Claim>() {
                new Claim("id", id.ToString()),
                new Claim("login", login)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("My super private key"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims:claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }

    }
}
