using System.ComponentModel.DataAnnotations;

namespace UniversityRegSystem.Shared.DTOS.CourseDTOS
{
    public class AddCourseDTO
    {
        public string Name { get; set; }

        public int NumberOfHours { get; set; }

        // relations
        public int? FieldId { get; set; }

        public int? PreviousCourseId { get; set; }
    }
}