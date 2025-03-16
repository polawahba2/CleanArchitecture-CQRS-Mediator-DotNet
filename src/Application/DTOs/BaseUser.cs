
using System.ComponentModel.DataAnnotations;


namespace Application.DTOs
{
    public class BaseUser
    {
        [Required, StringLength(50)]
        public string UserName { get; set; } = null!;

        [Required, StringLength(50)]
        public string Email { get; set; } = null!;

        [Required, StringLength(50)] 
        public string PhoneNumber { get; set; } = null!;
 

    }
}