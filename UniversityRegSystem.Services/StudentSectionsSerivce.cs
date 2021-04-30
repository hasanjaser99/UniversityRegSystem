using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegSystem.EntityFramework.Data;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class StudentSectionsSerivce : IStudentSectionsSerivce
    {
        private readonly ApplicationDbContext _context;

        public StudentSectionsSerivce()
        {
            _context = new ApplicationDbContext();
        }

        public List<StudentSectionsDTO> GetStudentSections(string studentId, string includeProperities = null)
        {
            var studentIdParameter = new SqlParameter("@StudentId", studentId);

            var studentSectionsDTO = _context.Database
                .SqlQuery<StudentSectionsDTO>("sp_studentSections @StudentId", studentIdParameter).ToList();

            return studentSectionsDTO;
        }
    }
}