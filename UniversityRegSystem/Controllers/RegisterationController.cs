using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;

namespace UniversityRegSystem.Controllers
{
    [Authorize(Roles = StaticData.Role_Student)]
    public class RegisterationController : Controller
    {
        private readonly IStudentRegisterationTimeService _service;

        private readonly IRegisterationService _registerationService;

        private readonly IStudentService _studentService;

        private readonly IStudentSectionsSerivce _studentSectionsService;


        public RegisterationController(
            IStudentRegisterationTimeService service,
            IRegisterationService registerationService,
            IStudentService studentService,
            IStudentSectionsSerivce studentSectionsService)
        {
            _service = service;
            _registerationService = registerationService;
            _studentService = studentService;
            _studentSectionsService = studentSectionsService;
        }

        // GET: Registeration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckRegisteration()
        {
            var studentId = User.Identity.GetUserId();

            var registerationTimes = _service.GetAllRegisterationTimesByTime(studentId,
                includeProperities: "RegisterationTime");

            if (registerationTimes.Count == 0)
            {
                return Json(new { success = false, ErrorMessage = "There is no registeration right now !" },
                    JsonRequestBehavior.AllowGet);
            }

            var studentSections = _studentSectionsService
                .GetStudentSections(studentId, includeProperities:"Section,Section.Course");

            return PartialView("~/Views/_RegisterationPartial.cshtml", studentSections);
        }

        public JsonResult GetFieldSections()
        {
            var studentId = User.Identity.GetUserId();

            var field = _studentService.GetStudentField(studentId, includeProperities:"Field");

            var fieldCourses = _registerationService.GetAllFieldSections(field.Id);

            return Json(new { success = true, data = fieldCourses }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCourse(int courseId, int sectionId)
        {
            var studentId = User.Identity.GetUserId();

            var response = await _registerationService.RegisterCourse(studentId, courseId, sectionId);

            if(response.IsSucsess)
            {
                return Json(new { success = true, message = "Course Registered Successfully" });
            }

            return Json(new { success = false, message = response.ErrorMessage});
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteCourse(int sectionId)
        {
            var studentId = User.Identity.GetUserId();

            var response = await _registerationService.DeleteCourse(studentId, sectionId);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while deleteing course" });
            }

            return Json(new { success = true, message = "Course un regsitered successfulyy" });
        }
    }
}