using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;

namespace UniversityRegSystem.Controllers
{
    [Authorize(Roles = StaticData.Role_Admin)]
    public class RegisterationTimeController : Controller
    {
        private readonly IRegisterationTimeService _service;

        public RegisterationTimeController(IRegisterationTimeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetRegisterationTimes()
        {
            var registerationTimes = _service.GetAllRegisterationTimes();

            return Json(new { data = registerationTimes }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertRegisterationTime(int? id)
        {
            var registerationTimeDTO = new RegisterationTimeDTO();

            if (id != null)
            {
                registerationTimeDTO = _service.GetRegisterationTimeById((int)id);
            }

            return View(registerationTimeDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpsertRegisterationTime(RegisterationTimeDTO registerationTimeDTO)
        {
            if (ModelState.IsValid)
            {
                if (registerationTimeDTO.Id != 0)
                {
                    var updateRegisterationTimeDTO = new UpdateRegisterationTimeDTO()
                    {
                        Id = registerationTimeDTO.Id,
                        StartTime = registerationTimeDTO.StartTime,
                        EndTime = registerationTimeDTO.EndTime,
                        Day = registerationTimeDTO.Day,
                    };

                    var response = await _service.UpdateRegisterationTime(updateRegisterationTimeDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        return View(registerationTimeDTO);
                    }
                }
                else
                {
                    var addRegisterationTimeDTO = new AddRegisterationTimeDTO()
                    {
                        StartTime = registerationTimeDTO.StartTime,
                        EndTime = registerationTimeDTO.EndTime,
                        Day = registerationTimeDTO.Day,
                    };

                    var response = await _service.CreateRegisterationTime(addRegisterationTimeDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        return View(registerationTimeDTO);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(registerationTimeDTO);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteRegisterationTime(int id)
        {
            var response = await _service.RemoveRegisterationTime(id);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while Deleting Registeration Time" });
            }
            return Json(new { success = true, message = "Registeration Time Deleted Successfully" });
        }
    }
}