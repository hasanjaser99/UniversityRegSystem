using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;
using UniversityRegSystem.Shared.DTOS.TeacherDTOS;

namespace UniversityRegSystem.Shared.DTOS.SectionDTOS
{
    [CustomValidation(typeof(SectionDTO), "TimesNotEqual")]
    public class SectionDTO
    {
        public int Id { get; set; }

        public byte Number { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        [Required]
        public string Days { get; set; }

        [Required]
        [Display(Name="Maximum Number Of Students")]
        [Range(1,40,ErrorMessage ="Value Should be between 1 and 40")]
        public int MaxNumberOfStudents { get; set; }

        public int NumberOfStudents { get; set; }

        //relations

        public Nullable<int> CourseId { get; set; }

        [ForeignKey("CourseId")]
        public CourseDTO Course { get; set; }

        [Required]
        public Nullable<int> TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public TeacherDTO Teacher { get; set; }

        public static ValidationResult TimesNotEqual(SectionDTO myEntity, ValidationContext validationContext)
        {
            if (myEntity.StartTime == myEntity.EndTime)
                return new ValidationResult("End Time Can't have the same value of Start Time");

            return ValidationResult.Success;
        }
    }

}
