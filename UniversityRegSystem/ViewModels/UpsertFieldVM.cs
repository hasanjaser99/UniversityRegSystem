using System.Collections.Generic;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;

namespace UniversityRegSystem.ViewModels
{
    public class UpsertFieldVM
    {
        public FieldDTO FieldDTO { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}