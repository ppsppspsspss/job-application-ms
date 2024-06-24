using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SocialMedia.API.Models
{
    public class JwtService
    {
        public string SecretKey { get; set; }
        public int TokenDuration { get; set; }
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration _configuration)
        {
            configuration = _configuration ?? throw new ArgumentNullException(nameof(_configuration));
            this.SecretKey = configuration.GetSection("JwtConfig")?.GetSection("Key")?.Value
                             ?? throw new InvalidOperationException("JwtConfig:Key configuration is missing.");
            this.TokenDuration = Int32.Parse(configuration.GetSection("JwtConfig")?.GetSection("Duration")?.Value
                             ?? throw new InvalidOperationException("JwtConfig:Duration configuration is missing."));
        }

        public string GenerateToken(string userID, string fullname, string phone, string email, string role)
        {
            if (userID == null) throw new ArgumentNullException(nameof(userID));
            if (fullname == null) throw new ArgumentNullException(nameof(fullname));
            if (phone == null) throw new ArgumentNullException(nameof(phone));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (role == null) throw new ArgumentNullException(nameof(role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("userID", userID),
                new Claim("fullname", fullname),
                new Claim("phone", phone),
                new Claim("email", email),
                new Claim("role", role)
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
