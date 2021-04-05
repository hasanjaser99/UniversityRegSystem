using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface ICourseService
    {
        Task<BaseResult> CreateCrouse(AddCourseDTO Model);

        Task<BaseResult> RemoveCourse(int id);

        Task<BaseResult> UpdateCourse(UpdateCourseDTO Model);

        CourseDTO GetCourseById(int id, string includeProperities = null);

        List<CourseDTO> GetAllCourses(string includeProperities = null);

        List<CourseDTO> GetAllCoursesByFieldId(int fieldId, string includeProperities = null);

        int? GetPreviousCourseId(int id);
    }
}