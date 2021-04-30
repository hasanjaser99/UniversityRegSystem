using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;
using UniversityRegSystem.Shared.DTOS.StudentDTOS;

namespace UniversityRegSystem.Shared.InterfaceServices
{
    public interface IStudentService
    {
        Task<BaseResult> CreateStudent(AddStudentDTO Model);

        Task<BaseResult> RemoveStudent(string id);

        Task<BaseResult> UpdateStudent(UpdateStudentDTO Model);

        StudentDTO GetStudentById(string id, string includeProperities = null);

        List<StudentDTO> GetAllStudents(string includeProperities = null);

        FieldDTO GetStudentField(string studentId, string includeProperities = null);
    }
}