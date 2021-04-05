using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityRegSystem.Models
{
    public class FieldCourse
    {
        [Key]
        public int Id { get; set; }

        //relations

        public int FieldId { get; set; }

        [ForeignKey("FieldId")]
        public Field Field { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public int TypeOfCourseId { get; set; }

        [ForeignKey("TypeOfCourseId")]
        public TypeOfCourse TypeOfCourse { get; set; }
    }
}
