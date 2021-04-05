using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS.DepartmentDTOS;

namespace UniversityRegSystem.Shared.DTOS.TeacherDTOS
{
    public class TeacherDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        // relations

        [Required]
        public Nullable<int> DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public DepartmentDTO Department { get; set; }

        //public IEnumerable<SectionDTO> Sections { get; set; }
    }
}
