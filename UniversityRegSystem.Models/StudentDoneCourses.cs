using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityRegSystem.Models
{
    public class StudentDoneCourses
    {
        [Key]
        [Column(Order = 1)]
        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}
