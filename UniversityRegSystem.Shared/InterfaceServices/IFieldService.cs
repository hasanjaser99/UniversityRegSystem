using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface IFieldService
    {
        Task<BaseResult> CreateField(AddFieldDTO Model);

        Task<BaseResult> RemoveField(int id);

        Task<BaseResult> UpdateField(UpdateFieldDTO Model);

        FieldDTO GetFieldById(int id, string includeProperities = null);

        List<FieldDTO> GetAllFields(string includeProperities = null);

        List<FieldDTO> GetAllFieldsByDepartmentId(int departmentId, string includeProperities = null);
    }
}