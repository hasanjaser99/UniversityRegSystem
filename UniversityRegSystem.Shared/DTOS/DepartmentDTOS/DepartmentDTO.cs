using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;

namespace UniversityRegSystem.Shared.DTOS.DepartmentDTOS
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<FieldDTO> Fields { get; set; }
    }
}