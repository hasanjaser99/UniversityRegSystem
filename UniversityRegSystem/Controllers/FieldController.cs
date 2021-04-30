using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;
using UniversityRegSystem.ViewModels;

namespace UniversityRegSystem.Controllers
{
    [Authorize(Roles = StaticData.Role_Admin)]
    public class FieldController : Controller
    {
        private readonly IFieldService _fieldService;
        private readonly IDepartmentService _departmentService;

        public FieldController(IFieldService service, IDepartmentService departmentService)
        {
            _fieldService = service;
            _departmentService = departmentService;
        }

        // GET: Course

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetFields()
        {
            var fields = _fieldService.GetAllFields(includeProperities: "Department");

            return Json(new { data = fields }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertField(int? id)
        {
            var upsertFieldVM = new UpsertFieldVM()
            {
                FieldDTO = new FieldDTO(),
                Departments = TransformDepartmentsToDropDownList()
            };

            if (id != null)
            {
                upsertFieldVM.FieldDTO = _fieldService.GetFieldById((int)id);
            }

            return View(upsertFieldVM);
        }

        [HttpPost]
        public async Task<ActionResult> UpsertField(UpsertFieldVM upsertFieldVM)
        {
            if (ModelState.IsValid)
            {
                if (upsertFieldVM.FieldDTO.Id != 0)
                {
                    var updateFieldDTO = new UpdateFieldDTO()
                    {
                        Id = upsertFieldVM.FieldDTO.Id,
                        Name = upsertFieldVM.FieldDTO.Name,
                        NumberOfHours = upsertFieldVM.FieldDTO.NumberOfHours,
                        DepartmentId = upsertFieldVM.FieldDTO.DepartmentId
                    };

                    var response = await _fieldService.UpdateField(updateFieldDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertFieldVM.Departments = TransformDepartmentsToDropDownList();

                        return View(upsertFieldVM);
                    }
                }
                else
                {
                    var addFieldDTO = new AddFieldDTO()
                    {
                        Name = upsertFieldVM.FieldDTO.Name,
                        NumberOfHours = upsertFieldVM.FieldDTO.NumberOfHours,
                        DepartmentId = upsertFieldVM.FieldDTO.DepartmentId
                    };

                    var response = await _fieldService.CreateField(addFieldDTO);

                    if (!response.IsSucsess)
                    {
                        ModelState.AddModelError(string.Empty, response.ErrorMessage);

                        upsertFieldVM.Departments = TransformDepartmentsToDropDownList();

                        return View(upsertFieldVM);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            upsertFieldVM.Departments = TransformDepartmentsToDropDownList();

            return View(upsertFieldVM);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteField(int id)
        {
            var response = await _fieldService.RemoveField(id);

            if (!response.IsSucsess)
            {
                return Json(new { success = false, message = "Error while Deleting Field" });
            }

            return Json(new { success = true, message = "Field Deleted Successfully" });
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