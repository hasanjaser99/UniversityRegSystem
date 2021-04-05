using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.ViewModels;
using Microsoft.AspNet.Identity;
using UniversityRegSystem.Shared.DTOS.StudentRegisterationTimeDTOS;

namespace UniversityRegSystem.Controllers
{
    public class StudentRegisterationTimeController : Controller
    {
        private readonly IStudentRegisterationTimeService _sRegService;

        private readonly IRegisterationTimeService _regService;

        public StudentRegisterationTimeController(IStudentRegisterationTimeService sRegService,
            IRegisterationTimeService regService)
        {
            _sRegService = sRegService;
            _regService = regService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var studentId = User.Identity.GetUserId();

            var studentRegisterationTimesVM = new StudentRegisterationTimesVM()
            {
                AllRegisterationTimes = _regService.GetRegisterationTimesByDay(),
                StudentRegisterationTimes = _sRegService
                .GetAllRegisterationTimes(studentId, includeProperities: "RegisterationTime")
            };

            return View(studentRegisterationTimesVM);
        }

        [HttpPost]
        public async Task<ActionResult> AddRegisterationTime(int registerationId)
        {
            var addStudentRegisterationTimeDTO = new AddStudentRegisterationTimeDTO()
            {
                RegisterationTimeId = registerationId,
                StudentId = User.Identity.GetUserId()
            };

            var response = await _sRegService.CreateRegisterationTime(addStudentRegisterationTimeDTO);

            if (!response.IsSucsess)
            {
                ModelState.AddModelError(string.Empty, response.ErrorMessage);

                return Json(new { success = false, message = "Error While Creating Registeration Time" });
            }

            return Json(new { success = true, message = "Registeration Time Created Successfully" });
        }
    }
}