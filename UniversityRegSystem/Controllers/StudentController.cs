using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.StudentDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.ViewModels;

namespace UniversityRegSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IFieldService _fieldService;


        public StudentController(IStudentService studentService,
            IDepartmentService departmentService,
            IFieldService fieldService)
        {
            _studentService = studentService;
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
        public JsonResult GetStudents()
        {
            var students = _studentService.GetAllStudents(includeProperities: "Field");

            return Json(new { data = students }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertStudent(string id)
        {
            var upsertStudentVM = new UpsertStudentVM()
            {
                Departments = TransformDepartmentsToDropDownList()
            };

            if(string.IsNullOrEmpty(id))
            {
                upsertStudentVM.Student = new StudentDTO();
                upsertStudentVM.Fields = FillFields();

            }else
            {
                upsertStudentVM.Student = _studentService.GetStudentById(id);

                var field = _fieldService.GetFieldById((int)upsertStudentVM.Student.FieldId);

                upsertStudentVM.SelectedField = field.Id;

                upsertStudentVM.SelectedDepartment = (int)field.DepartmentId;

                upsertStudentVM.Fields = FillFields(field.DepartmentId, field.Id);
            }

            return View(upsertStudentVM);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertStudent(UpsertStudentVM upsertStudentVM)
        {
            if(!string.IsNullOrEmpty(upsertStudentVM.Student.StudentId))
            {
                ModelState["Password"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(upsertStudentVM.Student.StudentId))
                {
                    var updateStudentDTO = new UpdateStudentDTO()
                    {
                        StudentId = upsertStudentVM.Student.StudentId,
                        Name = upsertStudentVM.Student.Name,
                        FieldId = upsertStudentVM.SelectedField,
                        Email = upsertStudentVM.Student.Email,
                        PhoneNumber = upsertStudentVM.Student.PhoneNumber,
                    };

                    var response = await _studentService.UpdateStudent(updateStudentDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertStudentVM.Departments = TransformDepartmentsToDropDownList();

                        upsertStudentVM.Fields = FillFields(upsertStudentVM.SelectedDepartment);

                        return View(upsertStudentVM);
                    }
                }
                else
                {
                    var addStudentDTO = new AddStudentDTO()
                    {
                        Name = upsertStudentVM.Student.Name,
                        FieldId = upsertStudentVM.SelectedField,
                        Email = upsertStudentVM.Student.Email,
                        PhoneNumber = upsertStudentVM.Student.PhoneNumber,
                        Password = upsertStudentVM.Password,
                    };

                    var response = await _studentService.CreateStudent(addStudentDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertStudentVM.Departments = TransformDepartmentsToDropDownList();

                        upsertStudentVM.Fields = FillFields(upsertStudentVM.SelectedDepartment);

                        return View(upsertStudentVM);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            upsertStudentVM.Departments = TransformDepartmentsToDropDownList();

            upsertStudentVM.Fields = FillFields(upsertStudentVM.SelectedDepartment);

            return View(upsertStudentVM);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteStudent(string id)
        {
            var response = await _studentService.RemoveStudent(id);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while Deleting Student" });
            }

            return Json(new { success = true, message = "Student Deleted Successfully" });
        }

        // Helper functions

        [HttpGet]
        public PartialViewResult PopulateFields(int depId)
        {
            var fieldsList = FillFields(depId);

            return PartialView("_FieldsSelectList", fieldsList);
        }

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