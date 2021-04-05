using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;
using UniversityRegSystem.Shared.DTOS.TypeOfCourseDTOS;

namespace UniversityRegSystem.Shared.DTOS.FieldCourseDTOS
{
    public class FieldCourseDTO
    {
        public int Id { get; set; }

        [Required]
        public int FieldId { get; set; }

        [ForeignKey("FieldId")]
        public FieldDTO Field { get; set; }

        [Required]
        [Display(Name ="Course")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public CourseDTO Course { get; set; }

        [Required]
        [Display(Name = "Type Of Course")]
        public int TypeOfCourseId { get; set; }

        [ForeignKey("TypeOfCourseId")]
        public TypeOfCourseDTO TypeOfCourse { get; set; }
    }
}