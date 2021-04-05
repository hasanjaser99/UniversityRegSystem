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
    public class AddStudentDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int FieldId { get; set; }

        public string Password { get; set; }
    }
}
