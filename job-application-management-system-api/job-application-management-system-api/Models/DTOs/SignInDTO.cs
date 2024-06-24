using System.ComponentModel.DataAnnotations;

namespace SocialMedia.API.Models.DTOs
{
    public class SignInDTO
    {
        [Required]
        [EmailAddress]
        public string? email { get; set; }

        [Required]
        public string? password { get; set; }
    }
}
