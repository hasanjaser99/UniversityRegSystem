using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityRegSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfHours { get; set; }

        // relations
        public Nullable<int> FieldId { get; set; }

        [ForeignKey("FieldId")]
        public Field Field { get; set; }

        public Nullable<int> PreviousCourseId { get; set; }

        [ForeignKey("PreviousCourseId")]
        public Course PreviousCourse { get; set; }

        public IEnumerable<Section> Sections { get; set; }
    }
}