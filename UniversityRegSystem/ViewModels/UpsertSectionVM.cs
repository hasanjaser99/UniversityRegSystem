using System.Collections.Generic;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.SectionDTOS;

namespace UniversityRegSystem.ViewModels
{
    public class UpsertSectionVM
    {
        public SectionDTO SectionDTO { get; set; }

        public IEnumerable<SelectListItem> Teachers { get; set; }

        public IEnumerable<SelectListItem> Days { get; set; }

        public int CourseId { get; set; }

        public int DepartmentId { get; set; }

    }
}