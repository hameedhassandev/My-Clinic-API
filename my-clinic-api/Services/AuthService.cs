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
        private readonly IAreaService _areaService;
        private readonly IHospitalService _hospitalService;
        private readonly IDepartmentService _departmentService;
        private readonly ISpecialistService _specialistService;
        private readonly IInsuranceService _insuranceService;
        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<JWT> jwt, IAreaService areaService, IHospitalService hospitalService,
            IDepartmentService departmentService, ISpecialistService specialistService, IInsuranceService insuranceService)
        {
            _userManager = userManager;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwt = jwt.Value;
            _areaService = areaService;
            _hospitalService = hospitalService;
            _departmentService = departmentService;
            _specialistService = specialistService;
            _insuranceService = insuranceService;
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
            var departmentIsExist = await _departmentService.DepartmentIsExists(doctorDto.DepartmentId);
            if (!departmentIsExist)
                return new AuthModelDto { Massage = "This department is not exists" };
            var specialist = await _departmentService.IsSpecialistInDepartment(doctorDto.DepartmentId, doctorDto.SpecialistsIds);
            if (!specialist)
                return new AuthModelDto { Massage = "Specialists are not exist" };
            
            var hospitals = await _hospitalService.IsHospitalIdsIsExist(doctorDto.HospitalsIds);
            if(!hospitals)
                return new AuthModelDto { Massage = "Hospitals are not exist" };

            var insurance = await _insuranceService.IsInsuranceIdsIsExist(doctorDto.InsuranceIds);
            if (!insurance)
                return new AuthModelDto { Massage = "Insurances are not exist" };

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

            string massage = "";
            if (isConfirmedFromAdmin)
                massage = "Doctor Data has confirmed and registered successfully!";
            else
                massage = "Doctor Data has registered successfully, and waiting for admin confirmation!";

            var doctorId = doctor.Id;
            //add spcialist to doctor
            foreach (int specialistId in doctorDto.SpecialistsIds)
            {
                await _specialistService.AddSpecialistToDoctor(doctorId, specialistId);
            }
            //add hospital to doctor
            foreach (int hosptalId in doctorDto.HospitalsIds)
            {
                await _hospitalService.AddHospitalToDoctor(doctorId, hosptalId);
            }
            //add insurance to doctor
            foreach (int insuranceId in doctorDto.InsuranceIds)
            {
                await _insuranceService.AddInsuranceToDoctor(doctorId, insuranceId);
            }

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
            var user = new Patient
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
    }
}
