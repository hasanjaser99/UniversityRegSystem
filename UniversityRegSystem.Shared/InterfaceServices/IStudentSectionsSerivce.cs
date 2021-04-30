using System.Collections.Generic;
using UniversityRegSystem.Shared.DTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface IStudentSectionsSerivce
    {
        List<StudentSectionsDTO> GetStudentSections(string studentId, string includeProperities = null);

    }
}