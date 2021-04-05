using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;
using UniversityRegSystem.Shared.DTOS.SectionDTOS;
using UniversityRegSystem.Shared.DTOS.TypeOfCourseDTOS;

namespace UniversityRegSystem.Shared.DTOS.CourseDTOS
{
    public class CourseDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int NumberOfHours { get; set; }

        // relations

        public Nullable<int> FieldId { get; set; }

        public FieldDTO Field { get; set; }

        public Nullable<int> PreviousCourseId { get; set; }

        public CourseDTO PreviousCourse { get; set; }

        public IEnumerable<SectionDTO> Sections { get; set; }
    }
}