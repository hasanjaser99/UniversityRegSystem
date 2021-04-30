using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;

namespace UniversityRegSystem.Controllers
{
    [Authorize(Roles = StaticData.Role_Student)]
    public class StudentSectionsController : Controller
    {
        private readonly IStudentSectionsSerivce _service;

        public StudentSectionsController(IStudentSectionsSerivce service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var studentId = User.Identity.GetUserId();

            var studentSections = _service.GetStudentSections(studentId,
                includeProperities: "Section,Section.Course");

            return View(studentSections);
        }
    }
}