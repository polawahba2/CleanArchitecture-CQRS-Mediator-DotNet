
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text; 
using Microsoft.Extensions.Options;
using Infrastructure.Authentication;
using Infrastructure.Identity;
using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Constants;

namespace Infrastructure.Authentication.services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;

        private readonly IMapper _mapper;
        public AuthServices(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt, IMapper mapper)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _mapper = mapper;
        }
        public async Task<UserDTO> RegisterAsync(RegistrationDTO registrationDTO)
        {

            var isUserNotExist = await _userManager.FindByEmailAsync(registrationDTO.Email);
            if (isUserNotExist is not null) return new UserDTO() { Message = "Email Is Already Exist" };


            var newUser = new ApplicationUser()
            {
                UserName = registrationDTO.UserName,
                Email = registrationDTO.Email,
                PhoneNumber = registrationDTO.PhoneNumber,
                Role = Roles.User,
            };
            var restult = await _userManager.CreateAsync(newUser, registrationDTO.Password);

            if (!restult.Succeeded)
            {
                var errors = restult.Errors.Select(e => e.Description);
                return new UserDTO()
                {
                    Message = String.Join(",", errors)
                };
            }

            await _userManager.AddToRoleAsync(newUser, Roles.User);
            var jwtToekn = await CreateJwtToken(newUser);

            return new UserDTO()
            {
                Email = newUser.Email,
                UserName = newUser.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToekn),
                IsAuthenticated = true,
                Message = "User Created Successfully",
                PhoneNumber = registrationDTO.PhoneNumber,
                Role = Roles.User,
            };
        }

        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            if (loginDTO is null) return new UserDTO() { Message = "Invalid Request" };

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null) return new UserDTO() { Message = "Invalid Email/Password" };

            var isUser = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isUser) return new UserDTO() { Message = "Invalid Email/Password" };


            var jwtToekn = await CreateJwtToken(user);

            var mappedUser= _mapper.Map<UserDTO>(user);
            mappedUser.Token = new JwtSecurityTokenHandler().WriteToken(jwtToekn);
            mappedUser.IsAuthenticated = true;
            mappedUser.Message = "Login Successful";
            return mappedUser;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}