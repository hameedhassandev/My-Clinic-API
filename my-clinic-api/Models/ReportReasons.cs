using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.Models
{
    public class ReportReasons
    {
        public int Id { get; set; }

        [Required]
        public string Reason { get; set; }
        public ICollection<Report>? Reports { get; set; }
    }
}
