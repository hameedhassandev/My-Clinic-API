using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace my_clinic_api.Models
{
    public class Doctor : ApplicationUser
    {

        [Required]
        [MaxLength(120)]
        [Display(Name = "Doctor Title")]
        public string? DoctorTitle { get; set; }

        [Display(Name = "Average Rate")]
        public float AvgRate { get; set; }


        [MaxLength(250)]
        public string Bio { get; set; } = string.Empty;

        public int NumberOfViews { get; set; }


        [Required]
        [Display(Name = "Wating Time in min.")]
        public int WaitingTime { get; set; }

        [Required]
        [Display(Name = "Confirmed from admin")]
        public bool IsConfirmedFromAdmin { get; set; }

        public DaysOfBlock ?DaysOfBlock { get; set; }

        public DateTime ? StartOfBlock { get; set; }
        public DateTime ? EndOfBlock { get; set; }


        public int DepartmentId { get; set; }


        public Department? Department { get; set; }

        public ICollection<Specialist>? Specialists { get; set; }
        public ICollection<TimesOfWork>? TimesOfWorks { get; set; }
        public ICollection<Insurance>? Insurances { get; set; }
        public virtual ICollection<Hospital>? Hospitals { get; set; }
        public ICollection<RateAndReview>? RatesAndReviews { get; set; }

    }
}
