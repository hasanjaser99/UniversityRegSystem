using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.DepartmentDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface IDepartmentService
    {
        Task<BaseResult> CreateDepartment(AddDepartmentDTO Model);

        Task<BaseResult> RemoveDepartment(int id);

        Task<BaseResult> UpdateDepartment(UpdateDepartmentDTO Model);

        DepartmentDTO GetDepartmentById(int id);

        List<DepartmentDTO> GetAllDepartments();
    }
}