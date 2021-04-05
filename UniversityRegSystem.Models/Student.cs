using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityRegSystem.Models
{
    public class Student
    {
        [Key]
        public string StudentId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        // relations

        public Nullable<int> FieldId { get; set; }

        [ForeignKey("FieldId")]
        public Field Field { get; set; }

        public IEnumerable<StudentDoneCourses> DoneCourses { get; set; }

        public IEnumerable<StudentRegisterationTimes> RegisterationTimes { get; set; }

        public IEnumerable<StudentSections> Sections { get; set; }
    }
}