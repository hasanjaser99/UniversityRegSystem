using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface IRegisterationService
    {
        List<RegisterationSectionDTO> GetAllFieldSections(int fieldId);

        Task<BaseResult> RegisterCourse(string studentId, int courseId, int sectionId);

        Task<BaseResult> DeleteCourse(string studentId, int sectionId);
    }
}
