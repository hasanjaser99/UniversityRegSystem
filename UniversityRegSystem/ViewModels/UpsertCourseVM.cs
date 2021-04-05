using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;

namespace UniversityRegSystem.ViewModels
{
    public class UpsertCourseVM
    {
        public CourseDTO Course { get; set; }

        public IEnumerable<SelectListItem> TypeOfCourses { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }

        public IEnumerable<SelectListItem> Fields { get; set; }

        public IEnumerable<SelectListItem> PreviousCourses { get; set; }

        public int SelectedDepartment { get; set; }

        [Required(ErrorMessage = "The Field field is required.")]
        public int? SelectedField { get; set; }

        public int? SelectedPreviousCourse { get; set; }
    }
}