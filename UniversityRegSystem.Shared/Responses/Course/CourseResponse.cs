using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;

namespace UniversityRegSystem.Shared.Responses.Course
{
    public class CourseResponse : BaseResult
    {
        public CourseDTO Data { get; set; }
    }
}