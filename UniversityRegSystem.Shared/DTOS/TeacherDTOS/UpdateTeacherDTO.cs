using System;

namespace UniversityRegSystem.Shared.DTOS.TeacherDTOS
{
    public class UpdateTeacherDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Nullable<int> DepartmentId { get; set; }
    }
}