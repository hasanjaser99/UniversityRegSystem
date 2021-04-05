using System.ComponentModel.DataAnnotations;

namespace UniversityRegSystem.Shared.DTOS.TypeOfCourseDTOS
{
    public class TypeOfCourseDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}