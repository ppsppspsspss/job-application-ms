using job_application_management_system_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.API.Models.DTOs;
using SocialMedia.API.Services;

namespace aiub_portal_api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-in")]
        public Result<string> SignIn([FromBody] SignInDTO signInDTO)
        {
            return this._authService.SignIn(signInDTO);
        }
    }
}