using System;
using System.ComponentModel.DataAnnotations;
using UniversityRegSystem.Shared.DTOS.DepartmentDTOS;

namespace UniversityRegSystem.Shared.DTOS.FieldDTOS
{
    public class FieldDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Number Of Hours")]
        [Range(132, 256, ErrorMessage = "Value Should be between 132 and 256")]
        public int NumberOfHours { get; set; }

        // relations

        [Required]
        public Nullable<int> DepartmentId { get; set; }

        public DepartmentDTO Department { get; set; }
    }
}