using job_application_management_system_api.Utils;
using SocialMedia.API.Models.DTOs;

namespace SocialMedia.API.Services
{
    public interface IAuthService
    {
        Result<string> SignIn(SignInDTO signInDTO);
    }
}