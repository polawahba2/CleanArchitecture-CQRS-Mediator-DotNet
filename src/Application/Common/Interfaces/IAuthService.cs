using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Common.Interfaces
{
    public interface IAuthServices
    {
        public Task<UserDTO> RegisterAsync(RegistrationDTO registrationDTO);
        public Task<UserDTO> LoginAsync(LoginDTO loginDTO);
    }
}