using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.SectionDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;
using UniversityRegSystem.ViewModels;

namespace UniversityRegSystem.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionService _sectionService;

        private readonly ITeacherService _teacherService;

        private readonly IFieldService _fieldService;

        private readonly ICourseService _courseService;

        private readonly IDepartmentService _departmentService;


        public SectionController(ISectionService service,
            ITeacherService teacherService,
            IFieldService fieldService,
            ICourseService courseService,
            IDepartmentService departmentService)
        {
            _sectionService = service;
            _teacherService = teacherService;
            _fieldService = fieldService;
            _courseService = courseService;
            _departmentService = departmentService;
        }

        // GET: Course

        [HttpGet]
        public ActionResult Index(int departmentId, int courseId)
        {
            if (!IsQueryParamsValid(courseId, departmentId))
            {
                return RedirectToAction("Index", "Course");
            }

            var sectionIndexVM = new SectionIndexVM()
            {
                DepartmentId = departmentId,
                CourseId = courseId
            };

            return View(sectionIndexVM);
        }

        [HttpGet]
        public JsonResult GetSections(int courseId)
        {
            var sections = _sectionService.GetAllSectionsByCourseId(courseId,includeProperities: "Teacher, Course");

            return Json(new { data = sections }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertSection(int departmentId, int courseId, int? id = null)
        {
            if (!IsQueryParamsValid(courseId, departmentId))
            {
                return RedirectToAction("Index", "Course");
            }

            var upsertSectionVM = new UpsertSectionVM()
            {
                SectionDTO = new SectionDTO(),
                Teachers = TransformTeachersToDropDownList(departmentId),
                CourseId = courseId,
                DepartmentId = departmentId,
                Days = StaticData.DaysList
            };

            if (id != null)
            {
                upsertSectionVM.SectionDTO = _sectionService.GetSectionById((int)id);
            }

            return View(upsertSectionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpsertSection(UpsertSectionVM upsertSectionVM)
        {

            if (ModelState.IsValid)
            {
                if (upsertSectionVM.SectionDTO.Id != 0)
                {
                    var updateSectionDTO = new UpdateSectionDTO()
                    {
                        Id = upsertSectionVM.SectionDTO.Id,
                        StartTime = upsertSectionVM.SectionDTO.StartTime,
                        EndTime = upsertSectionVM.SectionDTO.EndTime,
                        Days = upsertSectionVM.SectionDTO.Days,
                        MaxNumberOfStudents = upsertSectionVM.SectionDTO.MaxNumberOfStudents,
                        CourseId = upsertSectionVM.CourseId,
                        TeacherId = upsertSectionVM.SectionDTO.TeacherId,
                    };

                    var response = await _sectionService.UpdateSection(updateSectionDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertSectionVM.Teachers = TransformTeachersToDropDownList(upsertSectionVM.DepartmentId);

                        upsertSectionVM.Days = StaticData.DaysList;

                        return View(upsertSectionVM);
                    }
                }
                else
                {
                    var addSectionDTO = new AddSectionDTO()
                    {
                        StartTime = upsertSectionVM.SectionDTO.StartTime,
                        EndTime = upsertSectionVM.SectionDTO.EndTime,
                        Days = upsertSectionVM.SectionDTO.Days,
                        MaxNumberOfStudents = upsertSectionVM.SectionDTO.MaxNumberOfStudents,
                        CourseId = upsertSectionVM.CourseId,
                        TeacherId = upsertSectionVM.SectionDTO.TeacherId,
                    };

                    var response = await _sectionService.CreateSection(addSectionDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertSectionVM.Teachers = TransformTeachersToDropDownList(upsertSectionVM.DepartmentId);

                        upsertSectionVM.Days = StaticData.DaysList;

                        return View(upsertSectionVM);
                    }
                }

                return RedirectToAction(nameof(Index), 
                    new { departmentId = upsertSectionVM.DepartmentId, courseId = upsertSectionVM.CourseId });
            }

            upsertSectionVM.Teachers = TransformTeachersToDropDownList(upsertSectionVM.DepartmentId);

            upsertSectionVM.Days = StaticData.DaysList;

            return View(upsertSectionVM);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteSection(int id)
        {
            var response = await _sectionService.RemoveSection(id);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while Deleting Section" });
            }

            return Json(new { success = true, message = "Section Deleted Successfully" });
        }

        // Helper functions

        public IEnumerable<SelectListItem> TransformTeachersToDropDownList(int departmentId)
        {
            return _teacherService.GetAllTeachersByDepartmentId(departmentId).Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
        }

        public bool IsQueryParamsValid(int courseId, int departmentId)
        {
            var course = _courseService.GetCourseById(courseId);

            var department = _departmentService.GetDepartmentById(departmentId);

            if (course == null || department == null)
            {
                return false;
            }

            return true;
        }
    }
}