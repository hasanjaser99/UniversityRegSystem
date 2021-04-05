using System.Collections.Generic;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;

namespace UniversityRegSystem.Shared.Responses.Course
{
    public class ListOfCoursesResponse
    {
        public List<CourseDTO> Data { get; set; }
    }
}