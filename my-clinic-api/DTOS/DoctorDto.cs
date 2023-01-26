using my_clinic_api.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace my_clinic_api.DTOS
{
    public class DoctorDto 
    {
        public string? Id { get; set; }

        public byte[]? Image { get; set; }

        public string? Email { get; set; }
        public string? UserName { get; set; }

        public string? FullName { get; set; }
        public string? PhoneNo { get; set; }

        public Gender? Gender { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        public string? DoctorTitle { get; set; }
        public float AvgRate { get; set; }

        public string Bio { get; set; } = string.Empty;

        public int NumberOfViews { get; set; }

        public int WaitingTime { get; set; }
        public double Cost { get; set; }


        public bool IsConfirmedFromAdmin { get; set; }

        public DaysOfBlock? DaysOfBlock { get; set; }

        public DateTime? StartOfBlock { get; set; }
        public DateTime? EndOfBlock { get; set; }

        public Cities? Cities { get; set; }

        public Area? Area { get; set; }

        public string? Address { get; set; }


        public int DepartmentId { get; set; }

        public DepartmentDtoForDoctor? department { get; set; }
        public ICollection<SpecialistDto>? Specialists { get; set; }
        public ICollection<TimesOfWorkDto>? TimesOfWorks { get; set; }
        public ICollection<InsuranceDto>? Insurances { get; set; }
        public virtual ICollection<HospitalDto>? Hospitals { get; set; }
        public ICollection<RateAndReviewDto>? RatesAndReviews { get; set; }
        public ICollection<BookDto>? Bookings { get; set; }

    }
}
