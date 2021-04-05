using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS
{
    public class AddRegisterationTimeDTO
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Day { get; set; }
    }
}
