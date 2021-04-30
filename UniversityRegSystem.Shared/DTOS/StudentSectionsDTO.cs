using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityRegSystem.Shared.DTOS
{
    public class StudentSectionsDTO
    {
        public string CourseName { get; set; }

        public string Days { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string TeacherName { get; set; }

        public int SectionId { get; set; }
    }
}
