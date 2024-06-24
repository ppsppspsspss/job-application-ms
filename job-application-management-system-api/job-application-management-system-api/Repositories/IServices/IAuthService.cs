using SocialMedia.API.Models.DTOs;

namespace SocialMedia.API.Services
{
    public interface IAuthService
    {
        string SignIn(SignInDTO signInDTO);
    }
}