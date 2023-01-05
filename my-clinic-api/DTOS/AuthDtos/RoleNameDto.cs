using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS.AuthDtos
{
    public class RoleNameDto
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}
