using System.ComponentModel.DataAnnotations;

namespace UniversityRegSystem.Models
{
    public class TypeOfCourse
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}