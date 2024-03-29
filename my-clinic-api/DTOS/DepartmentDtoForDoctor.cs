﻿using System.ComponentModel.DataAnnotations;

namespace my_clinic_api.DTOS
{
    public class DepartmentDtoForDoctor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(255)]

        public string? Description { get; set; }

    }
}
