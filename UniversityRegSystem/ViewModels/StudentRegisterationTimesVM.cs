using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS;
using UniversityRegSystem.Shared.DTOS.StudentRegisterationTimeDTOS;

namespace UniversityRegSystem.ViewModels
{
    public class StudentRegisterationTimesVM
    {
        public IEnumerable<RegisterationTimeDTO> AllRegisterationTimes { get; set; }

        public IEnumerable<StudentRegisterationTimeDTO> StudentRegisterationTimes { get; set; }

    }
}