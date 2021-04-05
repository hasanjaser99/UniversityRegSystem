using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.TypeOfCourseDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface ITypeOfCourseService
    {
        Task<BaseResult> CreateTypeOfCourse(AddTypeOfCourseDTO Model);

        Task<BaseResult> RemoveTypeOfCourse(int id);

        Task<BaseResult> UpdateTypeOfCourse(UpdateTypeOfCourseDTO Model);

        TypeOfCourseDTO GetTypeOfCourseById(int id);

        List<TypeOfCourseDTO> GetAllTypeOfCourses();
    }
}