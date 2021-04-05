using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface IRegisterationTimeService
    {
        Task<BaseResult> CreateRegisterationTime(AddRegisterationTimeDTO Model);

        Task<BaseResult> RemoveRegisterationTime(int id);

        Task<BaseResult> UpdateRegisterationTime(UpdateRegisterationTimeDTO Model);

        RegisterationTimeDTO GetRegisterationTimeById(int id);

        List<RegisterationTimeDTO> GetAllRegisterationTimes();

        List<RegisterationTimeDTO> GetRegisterationTimesByDay();
    }
}