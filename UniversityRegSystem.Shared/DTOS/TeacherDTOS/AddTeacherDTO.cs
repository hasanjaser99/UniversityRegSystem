using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityRegSystem.Shared.DTOS.TeacherDTOS
{
    public class AddTeacherDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        public Nullable<int> DepartmentId { get; set; }
    }
}