using System;

namespace UniversityRegSystem.Shared.DTOS.FieldDTOS
{
    public class AddFieldDTO
    {
        public string Name { get; set; }

        public int NumberOfHours { get; set; }

        public Nullable<int> DepartmentId { get; set; }
    }
}