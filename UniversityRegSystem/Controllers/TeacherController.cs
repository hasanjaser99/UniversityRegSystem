using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.TeacherDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;
using UniversityRegSystem.ViewModels;

namespace UniversityRegSystem.Controllers
{
    [Authorize(Roles = StaticData.Role_Admin)]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IDepartmentService _departmentService;

        public TeacherController(ITeacherService teacherService, IDepartmentService departmentService)
        {
            _teacherService = teacherService;
            _departmentService = departmentService;
        }

        // GET: Course

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTeachers()
        {
            var teachers = _teacherService.GetAllTeachers(includeProperities: "Department");

            return Json(new { data = teachers }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertTeacher(int? id)
        {
            var upsertTeacherVM = new UpsertTeacherVM()
            {
                TeacherDTO = new TeacherDTO(),
                Departments = TransformDepartmentsToDropDownList()
            };

            if (id != null)
            {
                upsertTeacherVM.TeacherDTO = _teacherService.GetTeacherById((int)id);
            }

            return View(upsertTeacherVM);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertTeacher(UpsertTeacherVM upsertTeacherVM)
        {

            if (ModelState.IsValid)
            {
                if (upsertTeacherVM.TeacherDTO.Id != 0)
                {
                    var updateTeacherDTO = new UpdateTeacherDTO()
                    {
                        Id = upsertTeacherVM.TeacherDTO.Id,
                        Name = upsertTeacherVM.TeacherDTO.Name,
                        DepartmentId = upsertTeacherVM.TeacherDTO.DepartmentId,
                        Email = upsertTeacherVM.TeacherDTO.Email,
                        PhoneNumber = upsertTeacherVM.TeacherDTO.PhoneNumber,
                    };

                    var response = await _teacherService.UpdateTeacher(updateTeacherDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertTeacherVM.Departments = TransformDepartmentsToDropDownList();

                        return View(upsertTeacherVM);
                    }
                }
                else
                {
                    var addTeacherDTO = new AddTeacherDTO()
                    {
                        Name = upsertTeacherVM.TeacherDTO.Name,
                        DepartmentId = upsertTeacherVM.TeacherDTO.DepartmentId,
                        Email = upsertTeacherVM.TeacherDTO.Email,
                        PhoneNumber = upsertTeacherVM.TeacherDTO.PhoneNumber,
                    };

                    var response = await _teacherService.CreateTeacher(addTeacherDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertTeacherVM.Departments = TransformDepartmentsToDropDownList();

                        return View(upsertTeacherVM);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            upsertTeacherVM.Departments = TransformDepartmentsToDropDownList();

            return View(upsertTeacherVM);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteTeacher(int id)
        {
            var response = await _teacherService.RemoveTeacher(id);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while Deleting Teacher" });
            }

            return Json(new { success = true, message = "Teacher Deleted Successfully" });
        }

        // Helper functions

        public IEnumerable<SelectListItem> TransformDepartmentsToDropDownList()
        {
            return _departmentService.GetAllDepartments().Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
        }
    }
}