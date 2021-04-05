using System.Web.Mvc;
using UniversityRegSystem.Shared.DTOS.DepartmentDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDepartments()
        {
            var departments = _service.GetAllDepartments();

            return Json(new { data = departments }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpsertDepartment(int? id)
        {
            var departmentDTO = new DepartmentDTO();

            if (id != null)
            {
                departmentDTO = _service.GetDepartmentById((int)id);
            }

            return View(departmentDTO);
        }

        [HttpPost]
        public ActionResult UpsertDepartment(DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                if (departmentDTO.Id != 0)
                {
                    var updateDepartmentDTO = new UpdateDepartmentDTO()
                    {
                        Id = departmentDTO.Id,
                        Name = departmentDTO.Name
                    };

                    _service.UpdateDepartment(updateDepartmentDTO);
                }
                else
                {
                    var addDepartmentDTO = new AddDepartmentDTO()
                    {
                        Name = departmentDTO.Name
                    };

                    _service.CreateDepartment(addDepartmentDTO);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(departmentDTO);
        }

        [HttpDelete]
        public JsonResult DeleteDepartment(int id)
        {
            var response = _service.RemoveDepartment(id);

            //if(response.IsSucsess)

            return Json(new { success = true, message = "Department Deleted Successfully" });
        }
    }
}