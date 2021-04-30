using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityRegSystem.Models
{
    public class StudentRegisterationTimes
    {
        [Key]
        public int Id { get; set; }

        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int RegisterationTimeId { get; set; }

        [ForeignKey("RegisterationTimeId")]
        public RegisterationTime RegisterationTime { get; set; }

        public string Date { get; set; }
    }
}
