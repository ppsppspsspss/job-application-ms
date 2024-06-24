using SocialMedia.API.Data;
using SocialMedia.API.Models;
using SocialMedia.API.Models.DTOs;
using SocialMedia.API.Services;

namespace job_application_management_system_api.Repositories.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public string SignIn(SignInDTO signInDTO)
        {
            if (signInDTO == null)
            {
                throw new Exception("Invalid client request");
            }

            var user = _db.User.FirstOrDefault(u => u.email == signInDTO.email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.password != signInDTO.password)
            {
                throw new Exception("Incorrect password");
            }

            var token = new JwtService(_configuration).GenerateToken(user.userID.ToString(), user.fullname!, user.phone!, user.email!, user.role!);
            return token;

        }
    }
}