
using Application.DTOs;
using AutoMapper;
using Infrastructure.Identity;

namespace Infrastructure.Configurations
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}