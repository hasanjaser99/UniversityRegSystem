using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;
using UniversityRegSystem.Shared.DTOS.SectionDTOS;

namespace UniversityRegSystem.Shared.DTOS.StudentDTOS
{
    public class StudentDTO
    {
        public string StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        // relations

        public Nullable<int> FieldId { get; set; }

        [ForeignKey("FieldId")]
        public FieldDTO Field { get; set; }

        public IEnumerable<SectionDTO> Sections { get; set; }
    }
}
