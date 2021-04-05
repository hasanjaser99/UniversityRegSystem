using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegSystem.EntityFramework.Data;
using UniversityRegSystem.Models;
using UniversityRegSystem.Services.Mapper;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.DepartmentDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper mapper;

        public DepartmentService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
        }

        public async Task<BaseResult> CreateDepartment(AddDepartmentDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var department = new Department()
                {
                    Name = Model.Name,
                };

                _context.Departments.Add(department);

                await _context.SaveChangesAsync();

                result.IsSucsess = true;

                return result;
            }
            catch (Exception ex)
            {
                result.IsSucsess = false;
                result.ErrorMessage = "Internal Server Error";
                return result;
            }
        }

        public List<DepartmentDTO> GetAllDepartments()
        {
            var departments = _context.Departments.ToList();

            var departmentsDTO = mapper.Map<List<DepartmentDTO>>(departments);

            return departmentsDTO.ToList();
        }

        public DepartmentDTO GetDepartmentById(int id)
        {
            var department = _context.Departments.Find(id);

            var departmentDTO = mapper.Map<DepartmentDTO>(department);

            return departmentDTO;
        }

        public async Task<BaseResult> RemoveDepartment(int id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var department = _context.Departments.Find(id);

                if (department == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find department";
                    return result;
                }

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                result.IsSucsess = true;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSucsess = false;
                result.ErrorMessage = "Internal Server Error";
                return result;
            }
        }

        public async Task<BaseResult> UpdateDepartment(UpdateDepartmentDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var department = _context.Departments.Find(Model.Id);

                if (department == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find department";
                    return result;
                }

                department.Name = Model.Name;

                await _context.SaveChangesAsync();

                result.IsSucsess = true;

                return result;
            }
            catch (Exception ex)
            {
                result.IsSucsess = false;
                result.ErrorMessage = "Internal Server Error";
                return result;
            }
        }
    }
}