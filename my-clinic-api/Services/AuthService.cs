using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using my_clinic_api.Classes;
using my_clinic_api.Dto;
using my_clinic_api.Dto.AuthDtos;
using my_clinic_api.DTOS;
using my_clinic_api.DTOS.AuthDtos;
using my_clinic_api.Helpers;
using my_clinic_api.Interfaces;
using my_clinic_api.Models;
using my_clinic_api.Models.MailConfirmation;
using my_clinic_api.Services;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
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
        private readonly IDoctorService _doctorService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
       

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<JWT> jwt, IAreaService areaService, IHospitalService hospitalService,
            IDepartmentService departmentService, ISpecialistService specialistService,
            IInsuranceService insuranceService,RoleManager<IdentityRole> roleManager,
            IEmailSender emailSende, IDoctorService doctorServicer,ApplicationDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwt = jwt.Value;
            _areaService = areaService;
            _hospitalService = hospitalService;
            _departmentService = departmentService;
            _specialistService = specialistService;
            _insuranceService = insuranceService;
            _roleManager = roleManager;
            _emailSender = emailSende;
            _doctorService = doctorServicer;  
            _context = context;
        }

        async Task<DoctorDropDownDto> getDropDownForDoctor()
        {
            var ddlDto = new DoctorDropDownDto
            {
                Hospitals = await _context.Hospitals.ToListAsync(),
                Specialists = await _context.Specialists.ToListAsync(),  
                Insurances = await _context.Insurances.ToListAsync(),
                Areas = await _context.Areas.ToListAsync(), 
            };

            return ddlDto;
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
            if (!isAreaIdExist)return new AuthModelDto { Massage = "Area name is not right!" };

          

            var areaObj = new Area { Id = doctorDto.AreaId };
            var departmentIsExist = await _departmentService.DepartmentIsExists(doctorDto.DepartmentId);
            if (!departmentIsExist) return new AuthModelDto { Massage = "This department is not exists" };

            var specialist = await _departmentService.IsSpecialistInDepartment(doctorDto.DepartmentId, doctorDto.SpecialistsIds);
            if (!specialist) return new AuthModelDto { Massage = "Specialists are not exist" };

           /* var hospitals = await _hospitalService.IshospitalIdsIsExist(doctorDto.HospitalsIds);
            if (!hospitals) return new AuthModelDto { Massage = "Hospitals are not exist" };*/

            var insurance = await _insuranceService.IsInsuranceIdsIsExist(doctorDto.InsuranceIds);
            if (!insurance) return new AuthModelDto { Massage = "Insurances are not exist" };

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

            // mail confirmation
         /* var token  = await _userManager.GenerateEmailConfirmationTokenAsync(doctor);
            var confirmation = Url.Action()
             var confirmationLink = Url.Action("ConfirmEmail", "AuthController",
                     new { user = DoctorDto.Id, token = token }, Request.Scheme);*/

            string massage = "";
            if (isConfirmedFromAdmin)
                massage = "Doctor Data has confirmed and registered successfully!";
            else
                massage = "Doctor Data has registered successfully, and waiting for admin confirmation!";

            var doctorId = doctor.Id;

            var doctroData = await _context.doctors.FirstOrDefaultAsync(i=>i.Id == doctorId);

            AddSpecialistToDoctor(doctorDto.SpecialistsIds, doctorId);
            /* AddHospitalToDoctor(doctorDto.HospitalsIds, doctorId);*/
            AddInsuranceToDoctor(doctorDto.InsuranceIds, doctorId);
            foreach (var hospitalId in doctorDto.HospitalsIds)
            {
                try
                {
                    doctroData.Hospitals.Add(new Hospital { Id = hospitalId});
                }
                catch (Exception ex)
                {
                   
                }
            }
           


            return new AuthModelDto
            {
    
                Email = doctor.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuth = true,
                Roles = new List<string> { RoleNames.DoctorRole },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = doctor.UserName,
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

            var areaObj = new Area { Id = userDto.AreaId };
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
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new AuthModelDto { Massage = errors };

            }
            // if creation is success assign role to user
            await _userManager.AddToRoleAsync(user, RoleNames.PatientRole);

            var isSendEmail = await confirmationMail(user);
            if(isSendEmail) return new AuthModelDto { Massage = $"Registered successfully, check your Email : {user.Email}." };

         return new AuthModelDto { Massage = "Somthing error, try again" };
        }


        public async Task<AuthModelDto> GetTokenAsync(TokenRequestModelDto modelDto)
        {
            var authModel = new AuthModelDto();
            var user = await _userManager.FindByEmailAsync(modelDto.Email);
            //check if email is exist
            if (user is null || ! await _userManager.CheckPasswordAsync(user, modelDto.Password))
            {
                authModel.Massage = "Email or Password is incorrect";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            authModel.IsAuth = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = DateTime.Now;
            authModel.Roles = roles.ToList();
            authModel.Massage = "Login Successfully";

            return authModel;
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


        public async Task<bool> UserIsExist(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) return false;  

            return true;
        }


        public async Task<AuthModelDto> ConfirmDoctor(string doctorId)
        {
            var doctor = await _doctorService.FindDoctorByIdAsync(doctorId);
            if(doctor == null) return new AuthModelDto { Massage = "Doctor not found!", IsAuth = false };


            doctor.IsConfirmedFromAdmin = true;
            var result = await _userManager.UpdateAsync(doctor);
            if (!result.Succeeded) return new AuthModelDto { Massage = "Somthing error, not updated", IsAuth = false };


            return new AuthModelDto { Massage = "Doctor confirmed  successfuly" };  
        }


        public async Task<List<IdentityRole>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles;
        }


        public async Task<bool> AddRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
                return false;
            //if not exist add new role 
            await _roleManager.CreateAsync(new IdentityRole(roleName));
            return true;
        }


        private async void AddSpecialistToDoctor(List<int> SpecialistsIds, string doctorId)
        {
            //add spcialist to doctor

            foreach (int specialistId in SpecialistsIds)
            {
                await _specialistService.AddSpecialistToDoctor(doctorId, specialistId);
            }

        }

        
        private async void AddInsuranceToDoctor(List<int> InsuranceIds, string doctorId)
        {
            //add InsuranceIds to doctor

            foreach (int insuranceId in InsuranceIds)
            {
                await _specialistService.AddSpecialistToDoctor(doctorId, insuranceId);
            }

        }

        private async Task<bool> confirmationMail(ApplicationUser userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user is not null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
               // var result = await _userManager.ConfirmEmailAsync(user, token);
               // if (result.Succeeded)
               // {
                    var message = new Messages(new string[] { user.Email}, "Confirmation Email Link", "https://localhost:7041/swagger/index.html" );
                    _emailSender.SendEmail(message);
                    return true;
                //}
            }

            return false;
        }

        Task<DoctorDropDownDto> IAuthService.getDropDownForDoctor()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthModelDto> testRegisteration(DoctorRegisterDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null) return new AuthModelDto { Massage = "Email Alerdy Exist!" };

            throw new NotImplementedException();

        }
    }
}
