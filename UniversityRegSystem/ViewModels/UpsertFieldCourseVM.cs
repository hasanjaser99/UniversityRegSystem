using System.Collections.Generic;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.FieldCourseDTOS;

namespace UniversityRegSystem.ViewModels
{
    public class UpsertFieldCourseVM
    {
        public FieldCourseDTO FieldCourseDTO { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

        public IEnumerable<SelectListItem> TypeOfCourses { get; set; }

        public int FieldId { get; set; }
    }
}