using my_clinic_api.Models;

namespace my_clinic_api.Classes
{
    public class Filters
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string? SortOrder { get; set; }
        public string? DoctorName { get; set; }
        public string? DepartmenName { get; set; }
        public Area? Area { get; set; }

    }
}
