using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.StudentDTOS;

namespace UniversityRegSystem.ViewModels
{
    public class UpsertStudentVM
    {
        public StudentDTO Student { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }

        public IEnumerable<SelectListItem> Fields { get; set; }

        public int SelectedDepartment { get; set; }

        [Required(ErrorMessage = "The Field field is required.")]
        public int SelectedField { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and confirm password must match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}