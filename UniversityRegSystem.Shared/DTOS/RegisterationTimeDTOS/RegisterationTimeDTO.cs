using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS
{
    public class RegisterationTimeDTO
    {
        public int Id { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        [Required]
        public string Day { get; set; }
    }
}
