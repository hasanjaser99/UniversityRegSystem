using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;
using UniversityRegSystem.ViewModels;

namespace UniversityRegSystem.Controllers
{
    [Authorize(Roles =StaticData.Role_Admin)]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        private readonly ITypeOfCourseService _typeOfCourseService;

        private readonly IDepartmentService _departmentService;

        private readonly IFieldService _fieldService;

        public CourseController(ICourseService courseService,
            ITypeOfCourseService typeOfCourseService,
            IDepartmentService departmentService,
            IFieldService fieldService)
        {
            _courseService = courseService;
            _typeOfCourseService = typeOfCourseService;
            _departmentService = departmentService;
            _fieldService = fieldService;
        }

        // GET: Course

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCourses()
        {
            var courses = _courseService.GetAllCourses(includeProperities: "Field,PreviousCourse");

            return Json(new { data = courses }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertCourse(int? id)
        {
            var upsertCourseVM = new UpsertCourseVM()
            {
                Departments = TransformDepartmentsToDropDownList(),
                TypeOfCourses = TransformTypeOfCoursesToDropDownList(),
            };

            if (id == null)
            {
                // Create Course 

                upsertCourseVM.Course = new CourseDTO();
                upsertCourseVM.Fields = FillFields();
                upsertCourseVM.PreviousCourses = FillPreviousCourses();
            }
            else
            {
                // Edit Course 

                upsertCourseVM.Course = _courseService.GetCourseById((int)id);

                var field = _fieldService.GetFieldById((int)upsertCourseVM.Course.FieldId);

                upsertCourseVM.SelectedField = field.Id;

                upsertCourseVM.SelectedDepartment = (int)field.DepartmentId;

                upsertCourseVM.SelectedPreviousCourse = upsertCourseVM.Course.PreviousCourseId;

                upsertCourseVM.Fields = FillFields(field.DepartmentId, field.Id);

                upsertCourseVM.PreviousCourses = FillPreviousCourses(
                    field.Id, upsertCourseVM.SelectedPreviousCourse, upsertCourseVM.Course.Id);

            }

            return View(upsertCourseVM);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertCourse(UpsertCourseVM upsertCourseVM)
        {
            if (ModelState.IsValid)
            {
                if (upsertCourseVM.Course.Id != 0)
                {
                    var updateCourseDTO = new UpdateCourseDTO()
                    {
                        Id = upsertCourseVM.Course.Id,
                        Name = upsertCourseVM.Course.Name,
                        NumberOfHours = upsertCourseVM.Course.NumberOfHours,
                        FieldId = upsertCourseVM.SelectedField,
                        PreviousCourseId = upsertCourseVM.SelectedPreviousCourse,
                    };

                    var response = await _courseService.UpdateCourse(updateCourseDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        // If Error Occurred ( Re-Populate Drop Down Lists)
                        upsertCourseVM.Departments = TransformDepartmentsToDropDownList();
                        upsertCourseVM.TypeOfCourses = TransformTypeOfCoursesToDropDownList();
                        upsertCourseVM.Fields = FillFields(upsertCourseVM.SelectedDepartment);
                        upsertCourseVM.PreviousCourses = FillPreviousCourses(fieldId: upsertCourseVM.SelectedField);

                        return View(upsertCourseVM);
                    }

                }
                else
                {
                    var addCourseDTO = new AddCourseDTO()
                    {
                        Name = upsertCourseVM.Course.Name,
                        NumberOfHours = upsertCourseVM.Course.NumberOfHours,
                        FieldId = upsertCourseVM.SelectedField,
                        PreviousCourseId = upsertCourseVM.SelectedPreviousCourse,
                    };

                    var response = await _courseService.CreateCrouse(addCourseDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        // If Error Occurred ( Re-Populate Drop Down Lists)
                        upsertCourseVM.Departments = TransformDepartmentsToDropDownList();
                        upsertCourseVM.TypeOfCourses = TransformTypeOfCoursesToDropDownList();
                        upsertCourseVM.Fields = FillFields(upsertCourseVM.SelectedDepartment);
                        upsertCourseVM.PreviousCourses = FillPreviousCourses(fieldId: upsertCourseVM.SelectedField);

                        return View(upsertCourseVM);
                    }

                }

                return RedirectToAction(nameof(Index));

            }

            // If Error Occurred ( Re-Populate Drop Down Lists)
            upsertCourseVM.Departments = TransformDepartmentsToDropDownList();
            upsertCourseVM.TypeOfCourses = TransformTypeOfCoursesToDropDownList();
            upsertCourseVM.Fields = FillFields(upsertCourseVM.SelectedDepartment);
            upsertCourseVM.PreviousCourses = FillPreviousCourses(fieldId: upsertCourseVM.SelectedField);

            return View(upsertCourseVM);

        }

        [HttpDelete]
        public async Task<JsonResult> DeleteCourse(int id)
        {
            var response = await _courseService.RemoveCourse(id);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while deleting course" });

            }

            return Json(new { success = true, message = "Course Deleted Successfully" });
        }


        [HttpGet]
        public PartialViewResult PopulateFields(int depId)
        {
            var fieldsList = FillFields(depId);

            return PartialView("_FieldsSelectList", fieldsList);
        }

        // Helper functions

        public List<SelectListItem> FillFields(int? depId = null, int? Selected = null)
        {
            var fieldsList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "--Select Field--",
                    Disabled = true,
                    Selected = depId == null,
                    Value = null
                }
            };

            if (depId != null)
            {
                fieldsList.AddRange(_fieldService.GetAllFieldsByDepartmentId((int)depId)
                    .Select(f => new SelectListItem() 
                    { Text = f.Name, Value = f.Id.ToString(), Selected = f.Id == Selected }));
            }

            return fieldsList;
        }


        [HttpGet]
        public PartialViewResult PopulatePreviousCourses(int fieldId)
        {
            var previousCoursesList = FillPreviousCourses(fieldId:fieldId);

            return PartialView("_PreviousCourseSelectList", previousCoursesList);
        }

        public List<SelectListItem> FillPreviousCourses(int? fieldId = null , int? Selected = null, int? currentCourse = null)
        {
            var previousCoursesList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "None",
                    Selected = fieldId == null,
                    Value = null
                }
            };

            if (fieldId != null)
            {
                int? prevCourse = null;

                if(currentCourse != null)
                {
                    prevCourse = _courseService.GetPreviousCourseId((int)currentCourse);
                }

                previousCoursesList.AddRange(_courseService.GetAllCoursesByFieldId((int)fieldId)
                    .Where(c => c.Id != currentCourse && c.Id != prevCourse)
                    .Select(c => new SelectListItem() 
                    { Text = c.Name, Value = c.Id.ToString(), Selected = c.Id == Selected }));
            }

            return previousCoursesList;
        }

        public IEnumerable<SelectListItem> TransformDepartmentsToDropDownList()
        {
            return _departmentService.GetAllDepartments().Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            }).ToList();
        }

        public IEnumerable<SelectListItem> TransformTypeOfCoursesToDropDownList()
        {
            return _typeOfCourseService.GetAllTypeOfCourses().Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            }).ToList();
        }
    }
}