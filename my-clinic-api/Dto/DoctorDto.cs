using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace my_clinic_api.Dto
{
    public class DoctorDto 
    {
        public string? UserId { get; set; }

        public string? Email { get; set; }
        public string? UserName { get; set; }

        public string? Passowrd { get; set; }

        public string? FullName { get; set; }
        public string? DoctorTitle { get; set; }

        public float AvgRate { get; set; }

        public string Bio { get; set; } = string.Empty;

        public int NumberOfViews { get; set; }

        public int WaitingTime { get; set; }

        public bool IsConfirmedFromAdmin { get; set; }

        public DaysOfBlock? DaysOfBlock { get; set; }

        public DateTime? StartOfBlock { get; set; }
        public DateTime? EndOfBlock { get; set; }

        public Cities city { get; set; }
        public string? Area { get; set; }
        public string? Address { get; set; }

        public Gender? Gender { get; set; }
        public Department? Department { get; set; }

        public ApplicationUser? User { get; set; }

        public ICollection<Specialist>? Specialists { get; set; }
        public ICollection<TimesOfWork>? TimesOfWorks { get; set; }
        public ICollection<Insurance>? Insurances { get; set; }
        public ICollection<Hospital>? Hospitals { get; set; }
        public ICollection<RateAndReview>? RatesAndReviews { get; set; }
    }
}
