using my_clinic_api.Models;
using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class ReportReasonsDto
    {
        public int Id { get; set; }

        [Required]
        public string Reason { get; set; }
        
    }
}
