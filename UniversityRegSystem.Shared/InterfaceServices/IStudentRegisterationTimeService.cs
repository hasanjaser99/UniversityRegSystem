using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.StudentRegisterationTimeDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface IStudentRegisterationTimeService
    {
        Task<BaseResult> CreateRegisterationTime(AddStudentRegisterationTimeDTO Model);

        Task<BaseResult> RemoveRegisterationTime(string studentId, int registerationId);

        List<StudentRegisterationTimeDTO> GetAllRegisterationTimes(string studentId, string includeProperities = null);
    }
}