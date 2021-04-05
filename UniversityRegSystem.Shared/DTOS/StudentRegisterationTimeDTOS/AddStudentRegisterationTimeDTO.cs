using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS;
using UniversityRegSystem.Shared.DTOS.StudentDTOS;

namespace UniversityRegSystem.Shared.DTOS.StudentRegisterationTimeDTOS
{
    public class AddStudentRegisterationTimeDTO
    {
        public string StudentId { get; set; }

        public int RegisterationTimeId { get; set; }
    }
}
