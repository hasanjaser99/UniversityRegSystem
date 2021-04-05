using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.FieldCourseDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.ViewModels;

namespace UniversityRegSystem.Controllers
{
    public class FieldCourseController : Controller
    {
        private readonly IFieldCourseService _fieldCourseService;

        private readonly IFieldService _fieldService;

        private readonly ICourseService _courseService;

        private readonly ITypeOfCourseService _typeOfCourseService;


        public FieldCourseController(
            IFieldCourseService fieldCourseService,
            IFieldService fieldService,
            ICourseService courseService,
            ITypeOfCourseService typeOfCourseService)
        {
            _fieldCourseService = fieldCourseService;
            _fieldService = fieldService;
            _courseService = courseService;
            _typeOfCourseService = typeOfCourseService;
        }


       [HttpGet]
        public ActionResult Index(int fieldId)
        {
            if (!IsQueryParamsValid(fieldId))
            {
                return RedirectToAction("Index", "Field");
            }

            ViewBag.fieldId = fieldId;

            return View();
        }

        [HttpGet]
        public JsonResult GetFieldCourses(int fieldId)
        {
            var courses = _fieldCourseService.GetFieldCoursesByFieldId(fieldId, includeProperities: "Course,TypeOfCourse");

            return Json(new { data = courses }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertFieldCourse(int fieldId, int? id = null)
        {
            if (!IsQueryParamsValid(fieldId))
            {
                return RedirectToAction("Index", "Field");
            }

            var upsertFieldCourseVM = new UpsertFieldCourseVM()
            {
                FieldCourseDTO = new FieldCourseDTO(),
                Courses = TransformCoursesToDropDownList(),
                TypeOfCourses = TransformTypeOfCourseToDropDownList(),
                FieldId = fieldId
            };

            if (id != null)
            {
                upsertFieldCourseVM.FieldCourseDTO = _fieldCourseService.GetFieldCourseById((int)id);
            }

            return View(upsertFieldCourseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpsertFieldCourse(UpsertFieldCourseVM upsertFieldCourseVM)
        {

            if (ModelState.IsValid)
            {
                if (upsertFieldCourseVM.FieldCourseDTO.Id != 0)
                {
                    var updateFieldCourseDTO = new UpdateFieldCourseDTO()
                    {
                        Id = upsertFieldCourseVM.FieldCourseDTO.Id,
                        FieldId = upsertFieldCourseVM.FieldId,
                        CourseId = upsertFieldCourseVM.FieldCourseDTO.CourseId,
                        TypeOfCourseId = upsertFieldCourseVM.FieldCourseDTO.TypeOfCourseId,
                    };

                    var response = await _fieldCourseService.UpdateFieldCourse(updateFieldCourseDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertFieldCourseVM.Courses = TransformCoursesToDropDownList();

                        upsertFieldCourseVM.TypeOfCourses = TransformTypeOfCourseToDropDownList();

                        return View(upsertFieldCourseVM);
                    }
                }
                else
                {
                    var addFieldCourseDTO = new AddFieldCourseDTO()
                    {
                        FieldId = upsertFieldCourseVM.FieldId,
                        CourseId = upsertFieldCourseVM.FieldCourseDTO.CourseId,
                        TypeOfCourseId = upsertFieldCourseVM.FieldCourseDTO.TypeOfCourseId,
                    };

                    var response = await _fieldCourseService.CreateFieldCourse(addFieldCourseDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertFieldCourseVM.Courses = TransformCoursesToDropDownList();

                        upsertFieldCourseVM.TypeOfCourses = TransformTypeOfCourseToDropDownList();

                        return View(upsertFieldCourseVM);
                    }
                }

                return RedirectToAction(nameof(Index),new { fieldId = upsertFieldCourseVM.FieldId});
            }

            upsertFieldCourseVM.Courses = TransformCoursesToDropDownList();

            upsertFieldCourseVM.TypeOfCourses = TransformTypeOfCourseToDropDownList();

            return View(upsertFieldCourseVM);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteFieldCourse(int id)
        {
            var response = await _fieldCourseService.RemoveFieldCourse(id);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while Deleting Course" });
            }

            return Json(new { success = true, message = "Course Deleted Successfully" });
        }

        // Helper functions

        public IEnumerable<SelectListItem> TransformTypeOfCourseToDropDownList()
        {
            return _typeOfCourseService.GetAllTypeOfCourses().Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
        }

        public IEnumerable<SelectListItem> TransformCoursesToDropDownList()
        {
            return _courseService.GetAllCourses().Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
        }

        public bool IsQueryParamsValid(int fieldId)
        {
            var field = _fieldService.GetFieldById(fieldId);

            if (field == null)
            {
                return false;
            }

            return true;
        }
    }
}