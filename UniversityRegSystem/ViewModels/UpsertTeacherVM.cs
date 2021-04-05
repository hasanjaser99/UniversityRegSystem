using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.TeacherDTOS;

namespace UniversityRegSystem.ViewModels
{
    public class UpsertTeacherVM
    {
        public TeacherDTO TeacherDTO { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}