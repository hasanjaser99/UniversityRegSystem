using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UniversityRegSystem.Shared.Utility
{
    public class StaticData
    {
        public const string Role_Student = "Student";

        public const string Role_Admin = "Admin";

        public static List<SelectListItem> DaysList = new List<SelectListItem>() {
                     new SelectListItem{ Text="Sunday, Tuesday, Thursday", Value="Sunday, Tuesday, Thursday"},
                     new SelectListItem{ Text="Monday, Wednesday", Value="Monday, Wednesday"},
        };

        public static List<SelectListItem> Days = new List<SelectListItem>() {
                     new SelectListItem{ Text="Sunday", Value="Sunday"},
                     new SelectListItem{ Text="Monday", Value="Monday"},
                     new SelectListItem{ Text="Tuesday", Value="Tuesday"},
                     new SelectListItem{ Text="Wednesday", Value="Wednesday"},
                     new SelectListItem{ Text="Thursday", Value="Thursday"},
                     new SelectListItem{ Text="Friday", Value="Friday"},
        };
    }
}
