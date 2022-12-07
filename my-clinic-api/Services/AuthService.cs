using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using my_clinic_api.Classes;
using my_clinic_api.Dto;
using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.Helpers;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Models.RefreshTokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace my_clinic_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper,IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwt = jwt.Value;
        }
        public async Task<Doctor> DoctorRegisterAsync(DoctorRegisterDto doctorDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthModelDto> UserRegisterAsync(UserRegisterDto userDto)
        {
            //check if email is exist
            if (await _userManager.FindByEmailAsync(userDto.Email) is not null)
                return new AuthModelDto { Massage = "Email is alerdy register!" };

            //check if userName is exist
            if (await _userManager.FindByNameAsync(userDto.UserName) is not null)
                return new AuthModelDto { Massage = "UserName is alerdy register!" };
            //try to make with automapper latter
            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FullName = userDto.FullName,
                Cities = userDto.Cities,
                Area = userDto.Area,
                Address = userDto.Address,
                PhoneNo = userDto.PhoneNo,
                Gender = userDto.Gender,
                //image
            };
            //test mapper 
            //var user = _mapper.Map<ApplicationUser>(userDto);

            // now creat user
            var result = await _userManager.CreateAsync(user, userDto.Password);
            // in case creation is fail
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach(var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new AuthModelDto { Massage = errors };

            }
            // if creation is success assign role to user
            await _userManager.AddToRoleAsync(user, RoleNames.PatientRole);
            //create token
            var jwtSecurityToken = await CreateJwtToken(user);

            //refresh token
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken?.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            return new AuthModelDto{
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuth = true,
                Roles = new List<string> { RoleNames.PatientRole },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                Massage = "User register successfully"
            };
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
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
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
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
