using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.SectionDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface ISectionService
    {
        Task<BaseResult> CreateSection(AddSectionDTO Model);

        Task<BaseResult> RemoveSection(int id);

        Task<BaseResult> UpdateSection(UpdateSectionDTO Model);

        SectionDTO GetSectionById(int id, string includeProperities = null);

        List<SectionDTO> GetAllSections(string includeProperities = null);

        List<SectionDTO> GetAllSectionsByCourseId(int courseId, string includeProperities = null);
    }
}