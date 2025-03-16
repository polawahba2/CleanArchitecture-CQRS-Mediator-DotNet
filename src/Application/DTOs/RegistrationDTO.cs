using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RegistrationDTO:BaseUser
    {   
        [Required, StringLength(50)]
        public string Password { get; set; } = null!;
    }
}