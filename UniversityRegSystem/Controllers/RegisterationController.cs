using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Controllers
{
    public class RegisterationController : Controller
    {
        private readonly IStudentRegisterationTimeService _service;

        public RegisterationController(IStudentRegisterationTimeService service)
        {
            _service = service;
        }

        // GET: Registeration
        public ActionResult Index()
        {
            var studentId = User.Identity.GetUserId();

            var registerationTimes = _service.GetAllRegisterationTimesByTime
                (studentId, includeProperities: "RegisterationTime");

            return View();
        }
    }
}