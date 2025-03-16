
using System.ComponentModel.DataAnnotations;


namespace Application.DTOs
{
    public class LoginDTO
    {
        [Required, StringLength(50)]
        public string Email { get; set; } = null!;

        [Required, StringLength(50)]
        public string Password { get; set; } = null!;
    }
}