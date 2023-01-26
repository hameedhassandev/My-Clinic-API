using my_clinic_api.Models;

namespace my_clinic_api.DTOS
{
    public class SpecialistDepartmentDto
    {
        public int Id { get; set; }
        public string? SpecialistName { get; set; }

        public int DepartmentId { get; set; }
        public string? departmentName { get; set; }


    }
}
