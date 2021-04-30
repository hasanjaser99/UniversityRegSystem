using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.TypeOfCourseDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;

namespace UniversityRegSystem.Controllers
{
    [Authorize(Roles = StaticData.Role_Admin)]
    public class TypeOfCourseController : Controller
    {
        private readonly ITypeOfCourseService _service;

        public TypeOfCourseController(ITypeOfCourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTypeOfCourses()
        {
            var typeOfCourses = _service.GetAllTypeOfCourses();

            return Json(new { data = typeOfCourses }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertTypeOfCourse(int? id)
        {
            var typeOfCourseDTO = new TypeOfCourseDTO();

            if (id != null)
            {
                typeOfCourseDTO = _service.GetTypeOfCourseById((int)id);
            }

            return View(typeOfCourseDTO);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertTypeOfCourse(TypeOfCourseDTO typeOfCourseDTO)
        {
            if (ModelState.IsValid)
            {
                if (typeOfCourseDTO.Id != 0)
                {
                    var updateTypeOfCourseDTO = new UpdateTypeOfCourseDTO()
                    {
                        Id = typeOfCourseDTO.Id,
                        Name = typeOfCourseDTO.Name
                    };

                    var response = await _service.UpdateTypeOfCourse(updateTypeOfCourseDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        return View(typeOfCourseDTO);
                    }
                }
                else
                {
                    var addTypeOfCourseDTO = new AddTypeOfCourseDTO()
                    {
                        Name = typeOfCourseDTO.Name
                    };

                    var response = await _service.CreateTypeOfCourse(addTypeOfCourseDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        return View(typeOfCourseDTO);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(typeOfCourseDTO);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteTypeOfCourse(int id)
        {
            var response = await _service.RemoveTypeOfCourse(id);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while Deleting Type Of Course" });
            }
            return Json(new { success = true, message = "Type Of Course Deleted Successfully" });
        }
    }
}