using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS;
using UniversityRegSystem.Shared.DTOS.StudentDTOS;

namespace UniversityRegSystem.Shared.DTOS.StudentRegisterationTimeDTOS
{
    public class StudentRegisterationTimeDTO
    {
        public string StudentId { get; set; }

        public StudentDTO Student { get; set; }

        public int RegisterationTimeId { get; set; }

        public RegisterationTimeDTO RegisterationTime { get; set; }

        public DateTime Date { get; set; }
    }
}
