using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityRegSystem.Models
{
    public class Field
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfHours { get; set; }

        // relations
        public Nullable<int> DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<FieldCourse> FieldCourses { get; set; }
    }
}