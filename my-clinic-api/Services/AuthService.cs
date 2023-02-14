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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
        private new List<string> _allowsImageExtenstions = new List<string> { ".jpeg", ".png", ".jpg" };
        private long _allowMaxImageLength = 943718;//0.9MB
        private readonly ApplicationDbContext _context;



        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<JWT> jwt, IAreaService areaService, IHospitalService hospitalService,
            IDepartmentService departmentService, ISpecialistService specialistService,
            IInsuranceService insuranceService, RoleManager<IdentityRole> roleManager,
            IEmailSender emailSende, IDoctorService doctorServicer, ApplicationDbContext context)
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
            if (!isAreaIdExist) return new AuthModelDto { Massage = "Area name is not right!" };



            var areaObj = new Area { Id = doctorDto.AreaId };
            var departmentIsExist = await _departmentService.DepartmentIsExists(doctorDto.DepartmentId);
            if (!departmentIsExist) return new AuthModelDto { Massage = "This department is not exists" };

            var specialist = await _departmentService.IsSpecialistInDepartment(doctorDto.DepartmentId, doctorDto.SpecialistsIds);
            if (!specialist) return new AuthModelDto { Massage = "Specialists are not exist" };

            var hospitals = await _hospitalService.IsHospitalIdsIsExist(doctorDto.HospitalsIds);
            if (!hospitals) return new AuthModelDto { Massage = "Hospitals are not exist" };

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


            var doctorId = doctor.Id;
            var getDoctor = await _doctorService.FindDoctorByIdWithDataAsync(doctorId);

            var doc = await AddSpecialistToDoctor(doctorDto.SpecialistsIds, doctor);
            doc = await AddHospitalToDoctor(doctorDto.HospitalsIds, doc);
            doc = await AddInsuranceToDoctor(doctorDto.InsuranceIds, doc);
            _context.doctors.Update(doc);
            var count = _context.SaveChanges();

            return new AuthModelDto
            {

                Email = doctor.Email,
                IsAuth = true,
                Roles = new List<string> { RoleNames.DoctorRole },
                UserName = doctor.UserName,
                Massage = "Test"
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
                IsActive = true,
                //image = userDto.Image;
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

            //send email
            string testEmail = "hameedhassan9542@gmail.com";
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var message = new Messages(new string[] { testEmail }, "Authorize your email address", $"click to authorize your email: http://localhost:4200/valid-email/{token}/{userDto.UserName}");
            _emailSender.SendEmail(message);

            return new AuthModelDto { Massage = "Register succsessfully, check your email", IsAuth = true };
        }


        public async Task<bool> ConfirmUserEmail(confirmMailDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email);
            if(user == null) return false;
            var result = await _userManager.ConfirmEmailAsync(user, dto.Token);
            if (!result.Succeeded) return false;
            return true;
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
            var isMailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isMailConfirmed)
            {
                authModel.Massage = "Invalid login attempt!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Id = user.Id;
            authModel.IsAuth = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
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


        private async Task<bool> UserNameIsExist(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null) return false;  

            return true;
        }

        public async Task<bool> EmailIsExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

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


        private async Task<Doctor> AddSpecialistToDoctor(List<int> SpecialistsIds , Doctor doctor )
        {
            return await _specialistService.AddSpecialistToDoctor(SpecialistsIds,doctor);
        }


        private async Task<Doctor> AddInsuranceToDoctor(List<int> InsuranceIds, Doctor doctor)
        {
            return await _insuranceService.AddInsuranceToDoctor(InsuranceIds, doctor);
        }

        private async Task<Doctor> AddHospitalToDoctor(List<int> SpecialistsIds, Doctor doctor)
        {
            return await _hospitalService.AddHospitalToDoctor(SpecialistsIds, doctor);
        }

        public async Task<bool> confirmationMailDoctor(string doctorId)
        {
            var doctor = await _userManager.FindByIdAsync(doctorId);

            if (doctor is not null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(doctor);
                var result = await _userManager.ConfirmEmailAsync(doctor, token);
               if (result.Succeeded)
               {
                   // string testEmail = "hameedhassan9542@gmail.com";
                    string testEmail = "Hameed_20180216 @fci.helwan.edu.eg";

                    //var message = new Messages(new string[] { doctor.Email}, "Accept To Join My Clinic Email", $"Congrats Doctor: {doctor.FullName} you are accepted to join My clinic visit: http://localhost:4200/login");
                    var message = new Messages(new string[] { testEmail }, "Accept To Join My Clinic", $"Congrats Doctor: {doctor.FullName} you are accepted to join My clinic visit: http://localhost:4200/login");
                    _emailSender.SendEmail(message);
                    return true;
               }
            }

            return false;
        }

        private bool allowExtenstions(string path)
        {
            if (!_allowsImageExtenstions.Contains(path)) return false;
            return true;
        }
        private bool allowImageMaxLength(long imageLength)
        {
            if(imageLength > _allowMaxImageLength) return false;
            return true;
        }

        public async Task<AuthModelDto> testRegisteration(DoctorRegisterDto dto)
        {
            if (await EmailIsExist(dto.Email)) return new AuthModelDto { Massage = "Email is exist!"};
            if(await UserNameIsExist(dto.UserName)) return new AuthModelDto { Massage = "Username is exist!"};
            if (!allowExtenstions(Path.GetExtension(dto.Image.FileName).ToLower())) return new AuthModelDto { Massage = "Only .jpeg, .jpg and .png are allowed" };
            if (!allowImageMaxLength(dto.Image.Length)) return new AuthModelDto { Massage = "Max image allowed size is 0.9 MB" };

            using var dataStream = new MemoryStream();
            await dto.Image.CopyToAsync(dataStream);

            var doctor = new Doctor
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                DoctorTitle = dto.DoctorTitle,
                //Bio = dto.Bio,
                Cost = dto.Cost,
                WaitingTime = dto.WaitingTime,
                DepartmentId = dto.DepartmentId,
                Cities = dto.Cities,
                AreaId = dto.AreaId,
                Address = dto.Address,
                PhoneNo = dto.PhoneNo,
                Gender = dto.Gender,
                IsActive = true,
                Image = dataStream.ToArray(),
            };

            var result = await _userManager.CreateAsync(doctor, dto.Password);

            var err = ErrorOfIdentityResult(result);
            if (!err.IsNullOrEmpty()) return new AuthModelDto { Massage = err };

            await _userManager.AddToRoleAsync(doctor, RoleNames.DoctorRole);
            try
            {
                var doc = await AddSpecialistToDoctor(dto.SpecialistsIds, doctor);
                doc = await AddHospitalToDoctor(dto.HospitalsIds, doc);
                doc = await AddInsuranceToDoctor(dto.InsuranceIds, doc);
                _context.doctors.Update(doc);
                var count = _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return new AuthModelDto { Massage = $"Somthing error, try again!, {ex}" };

            }
    
            return new AuthModelDto { Massage = $"Follow your email {doctor.Email} until approval to join is sent from the admin.",IsAuth=true };
        }

        public async Task<AuthModelDto> updatDoctor(updateDoctorDto dto)
        {
            var doctor = await _doctorService.FindDoctorByIdWithDataAsync(dto.doctorId);
            if (doctor == null) return new AuthModelDto { Massage = $"No doctor with Id {dto.doctorId}" };

            doctor.DoctorTitle = dto.DoctorTitle;
            doctor.PhoneNo = dto.PhoneNo;
            doctor.Cost = dto.Cost;
            doctor.WaitingTime = dto.WaitingTime;

            try
            {
               var result =  _context.doctors.Update(doctor);
                _context.SaveChanges();
            }
            catch
            {
                return new AuthModelDto { Massage = "Somthing error, try again!" };
            }

            return new AuthModelDto { Massage = "Doctor updated successfully", IsAuth = true };
        }


        public async Task<AuthModelDto> updateProfilePic(updateImageDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.userId);
            if (user == null) return new AuthModelDto { Massage = $"User Id {dto.userId} not exist!" };
            if (!allowExtenstions(Path.GetExtension(dto.Image.FileName).ToLower())) return new AuthModelDto { Massage = "Only .jpeg, .jpg and .png are allowed" };
            if (!allowImageMaxLength(dto.Image.Length)) return new AuthModelDto { Massage = "Max image allowed size is 0.9 MB" };

            using var dataStream = new MemoryStream();
            await dto.Image.CopyToAsync(dataStream);
            //convert to array
            var image = dataStream.ToArray();
            user.Image = image;
            var result = await _userManager.UpdateAsync(user);
            var err = ErrorOfIdentityResult(result);
            if (!err.IsNullOrEmpty()) return new AuthModelDto { Massage = err };

            return new AuthModelDto { Massage = "Image updated successfully", IsAuth = true };
        }

        
        private string ErrorOfIdentityResult(IdentityResult result)
        {
            var errors = string.Empty;
            if (result.Succeeded)
                return String.Empty;

            foreach (var error in result.Errors)
                errors += $"{error.Description}, ";

            return errors;
        }



    }



}
