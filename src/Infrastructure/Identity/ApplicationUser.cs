using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {

        [Required,MaxLength(10)]
        public String Role { get; set; } = Roles.User;
    }
}