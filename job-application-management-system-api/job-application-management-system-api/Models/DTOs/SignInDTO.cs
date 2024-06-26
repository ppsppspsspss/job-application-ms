using System.ComponentModel.DataAnnotations;

namespace SocialMedia.API.Models.DTOs
{
    public class SignInDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
