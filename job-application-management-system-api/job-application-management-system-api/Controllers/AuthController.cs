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

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public ActionResult SignIn([FromBody] SignInDTO signInDTO)
        {
            try
            {
                var token = _authService.SignIn(signInDTO);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}