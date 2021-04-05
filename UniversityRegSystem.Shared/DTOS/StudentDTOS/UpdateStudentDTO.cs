namespace UniversityRegSystem.Shared.DTOS.StudentDTOS
{
    public class UpdateStudentDTO
    {
        public string StudentId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int FieldId { get; set; }
    }
}