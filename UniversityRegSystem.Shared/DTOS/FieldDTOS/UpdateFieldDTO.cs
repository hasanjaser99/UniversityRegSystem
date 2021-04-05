using System;

namespace UniversityRegSystem.Shared.DTOS.FieldDTOS
{
    public class UpdateFieldDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfHours { get; set; }

        public Nullable<int> DepartmentId { get; set; }
    }
}