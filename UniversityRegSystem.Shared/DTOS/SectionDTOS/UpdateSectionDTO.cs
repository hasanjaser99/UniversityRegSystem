using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityRegSystem.Shared.DTOS.SectionDTOS
{
    public class UpdateSectionDTO
    {
        public int Id { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Days { get; set; }

        public int MaxNumberOfStudents { get; set; }

        public Nullable<int> CourseId { get; set; }

        public Nullable<int> TeacherId { get; set; }
    }
}
