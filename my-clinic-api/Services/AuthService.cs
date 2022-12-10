using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using my_clinic_api.Classes;
using my_clinic_api.Dto;
using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.Helpers;
using my_clinic_api.Interfaces;
using my_clinic_api.Migrations;
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
        private readonly IAreaService _areaService;
        private readonly ApplicationDbContext _context;
        string massage = "";
        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<JWT> jwt,
            IAreaService areaService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwt = jwt.Value;
            _areaService = areaService;
            _context = context; 
        }

        public async Task<AuthModelDto> DoctorRegisterAsync(DoctorRegisterDto doctorDto, bool isConfirmedFromAdmin)
        {
            //check if email is exist
            if (await _userManager.FindByEmailAsync(doctorDto.Email) is not null)
                return new AuthModelDto { Massage = "Email is alerdy register!" };

            //check if userName is exist
            if (await _userManager.FindByNameAsync(doctorDto.UserName) is not null)
                return new AuthModelDto { Massage = "UserName is alerdy register!" };

            //chech area is exist
            var isAreaIdExist = await _areaService.AreaIdIsExist(doctorDto.AreaId);
            if (!isAreaIdExist)
                return new AuthModelDto { Massage = "Area name is not right!" };

            var areaObj = new Area { Id = doctorDto.AreaId };
            var listOfSpicealisId = doctorDto.SpecialistIds;
            var listOfHospitalId = doctorDto.HospitalIds;
            var listOfInsuranceId = doctorDto.InsuranceIds;
            var doctor = new Doctor
            {
                UserName = doctorDto.UserName,
                Email = doctorDto.Email,
                FullName = doctorDto.FullName,
                DoctorTitle = doctorDto.DoctorTitle,
                Bio = doctorDto.Bio,
                Cost = doctorDto.Cost,
                WaitingTime = doctorDto.WaitingTime,
                DepartmentId = doctorDto.DepartmentId,
                Cities = doctorDto.Cities,
                AreaId = doctorDto.AreaId,
                Address = doctorDto.Address,
                PhoneNo = doctorDto.PhoneNo,
                Gender = doctorDto.Gender,
                IsConfirmedFromAdmin = isConfirmedFromAdmin,
                IsActive = true,
               
                //image
            };

            // now creat user
            var result = await _userManager.CreateAsync(doctor, doctorDto.Password);

            //var to get doctorId to add doctor_spcialist

           
            //AddDoctorSpecialist(doctorId, 1);

            // in case creation is fail 
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new AuthModelDto { Massage = errors };

            }


            // if creation is success assign role to doctor
            await _userManager.AddToRoleAsync(doctor, RoleNames.DoctorRole);
            //create token
            var jwtSecurityToken = await CreateJwtToken(doctor);

            //refresh token
            var refreshToken = GenerateRefreshToken();
            doctor.RefreshToken?.Add(refreshToken);
            await _userManager.UpdateAsync(doctor);

            //add doctor specialist
            var doctorId = doctor.Id;

            foreach(var hs in listOfHospitalId)
            {
                AddDoctorHospital(doctorId, hs);
            }

            foreach (var sp in listOfSpicealisId)
            {
                AddDoctorSpecialist(doctorId, sp);
            }

            foreach (var ins in listOfInsuranceId)
            {
                AddDoctorInsurance(doctorId, ins);
            }



            
            if (isConfirmedFromAdmin)
                massage = "Doctor Data has confirmed and registered successfully!";
            else
                massage = "Doctor Data has registered successfully, and waiting for admin confirmation!";

            return new AuthModelDto
            {
                Email = doctor.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuth = true,
                Roles = new List<string> { RoleNames.DoctorRole },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = doctor.UserName,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                Massage = massage
            };
        }

        public async Task<AuthModelDto> UserRegisterAsync(UserRegisterDto userDto)
        {
            //check if email is exist
            if (await _userManager.FindByEmailAsync(userDto.Email) is not null)
                return new AuthModelDto { Massage = "Email is alerdy register!" };

            //check if userName is exist
            if (await _userManager.FindByNameAsync(userDto.UserName) is not null)
                return new AuthModelDto { Massage = "UserName is alerdy register!" };

            //chech area is exist
            var isAreaIdExist = await _areaService.AreaIdIsExist(userDto.AreaId);
            if (!isAreaIdExist)
                return new AuthModelDto { Massage = "Area name is not right!" };

            var areaObj = new Area { Id = userDto.AreaId};
            //try to make with automapper latter
            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FullName = userDto.FullName,
                Cities = userDto.Cities,
                AreaId = userDto.AreaId,
                Address = userDto.Address,
                PhoneNo = userDto.PhoneNo,
                Gender = userDto.Gender,
                IsActive = true
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

        private void AddDoctorSpecialist(string doctorId, int specialistId)
        {
            var isExist = _context.Specialists.FirstOrDefault(i => i.Id == specialistId);
            if (isExist == null)
                return;
            var doctorSpObj = new Models.M2M.Doctor_Specialist { DoctorId = doctorId, SpecialistId = specialistId };

            _context.Doctor_Specialist.Add(doctorSpObj);

            _context.SaveChanges();
        }

        private void AddDoctorHospital(string doctorId, int hospitalId)
        {
            var isExist = _context.Hospitals.FirstOrDefault(i => i.Id == hospitalId);
            if (isExist == null)
                return;
            var doctorHsObj = new Models.M2M.Doctor_Hospital { DoctorId = doctorId, HospitalId = hospitalId };

            _context.Doctor_Hospital.Add(doctorHsObj);

            _context.SaveChanges();
        }

        private void AddDoctorInsurance(string doctorId, int insuranceId)
        {
            var isExist = _context.Insurances.FirstOrDefault(i => i.Id == insuranceId);
            if(isExist == null)
                return; 

            var doctorInObj = new Models.M2M.Doctor_Insurance { DoctorId = doctorId, InsuranceId = insuranceId };

            _context.Doctor_Insurance.Add(doctorInObj);

            _context.SaveChanges();
        }
    }
}
