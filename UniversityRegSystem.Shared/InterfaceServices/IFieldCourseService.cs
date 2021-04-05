using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.FieldCourseDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface IFieldCourseService
    {
        Task<BaseResult> CreateFieldCourse(AddFieldCourseDTO Model);

        Task<BaseResult> RemoveFieldCourse(int id);

        Task<BaseResult> UpdateFieldCourse(UpdateFieldCourseDTO Model);

        List<FieldCourseDTO> GetAllFieldCourses(string includeProperities = null);

        FieldCourseDTO GetFieldCourseById(int id, string includeProperities = null);

        List<FieldCourseDTO> GetFieldCoursesByFieldId(int fieldId, string includeProperities = null);
    }
}