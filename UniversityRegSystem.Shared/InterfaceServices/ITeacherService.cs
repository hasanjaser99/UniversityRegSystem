using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.TeacherDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface ITeacherService
    {
        Task<BaseResult> CreateTeacher(AddTeacherDTO Model);

        Task<BaseResult> RemoveTeacher(int id);

        Task<BaseResult> UpdateTeacher(UpdateTeacherDTO Model);

        TeacherDTO GetTeacherById(int id, string includeProperities = null);

        List<TeacherDTO> GetAllTeachers(string includeProperities = null);

        List<TeacherDTO> GetAllTeachersByDepartmentId(int departmentId);
    }
}