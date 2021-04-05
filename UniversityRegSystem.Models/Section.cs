using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityRegSystem.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }

        public byte Number { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Days { get; set; }

        public int MaxNumberOfStudents { get; set; }

        public int NumberOfStudents { get; set; }

        //relations

        public Nullable<int> CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public Nullable<int> TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
    }
}