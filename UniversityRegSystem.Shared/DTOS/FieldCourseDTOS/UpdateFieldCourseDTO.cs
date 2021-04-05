using System;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;
using UniversityRegSystem.Shared.DTOS.TypeOfCourseDTOS;

namespace UniversityRegSystem.Shared.DTOS.FieldCourseDTOS
{
    public class UpdateFieldCourseDTO
    {
        public int Id { get; set; }

        public int FieldId { get; set; }

        public int CourseId { get; set; }

        public int TypeOfCourseId { get; set; }
    }
}