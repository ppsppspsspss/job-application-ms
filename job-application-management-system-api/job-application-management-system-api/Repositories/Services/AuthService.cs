using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SocialMedia.API.Data;
using SocialMedia.API.Models.DTOs;
using SocialMedia.API.Services;
using job_application_management_system_api.Utils;
using SocialMedia.API.Models;

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

        public Result<string> SignIn(SignInDTO signInDTO)
        {
            if (signInDTO == null)
            {
                return new Result<string>(true, new List<string> { "Invalid client request" }, null);
            }

            var user = _db.User.FirstOrDefault(u => u.Email == signInDTO.Email);

            if (user == null)
            {
                return new Result<string>(true, new List<string> { "User not found" }, null);
            }

            if (user.Password != signInDTO.Password)
            {
                return new Result<string>(true, new List<string> { "Incorrect password" }, null);
            }

            var token = new JwtService(_configuration).GenerateToken(user.UserID.ToString(), user.Fullname, user.Phone, user.Email, user.Role);
            return new Result<string>(false, new List<string> { "Sign in successful" }, token);
        }
    }
}
