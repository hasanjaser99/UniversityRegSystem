using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityRegSystem.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Field> Fields { get; set; }
    }
}