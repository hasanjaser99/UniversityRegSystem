using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegSystem.EntityFramework;
using UniversityRegSystem.EntityFramework.Data;
using UniversityRegSystem.Models;
using UniversityRegSystem.Services.Mapper;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.TeacherDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;

namespace UniversityRegSystem.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<Teacher> dbSet;

        private readonly IMapper mapper;


        public TeacherService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
            this.dbSet = _context.Set<Teacher>();
        }

        public async Task<BaseResult> CreateTeacher(AddTeacherDTO Model)
        {

            BaseResult response = new BaseResult();
            try
            {
                var userModel = new ApplicationUser()
                {
                    UserName = Model.Email,
                    Email = Model.Email,
                    PhoneNumber = Model.PhoneNumber,
                };

                var teacher = new Teacher()
                {
                    Name = Model.Name,
                    Email = Model.Email,
                    PhoneNumber = Model.PhoneNumber,
                    DepartmentId = Model.DepartmentId
                };

                _context.Teachers.Add(teacher);

                await _context.SaveChangesAsync();

                response.IsSucsess = true;

                return response;
            }
            catch (Exception ex)
            {
                response.IsSucsess = false;
                response.ErrorMessage = "Internal Server Error";
                return response;
            }
        }

        public List<TeacherDTO> GetAllTeachers(string includeProperities = null)
        {
            IQueryable<Teacher> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<Teacher>.IncludeProps(includeProperities, query);
            }

            var teachers = query.ToList();

            var teachersDTO = mapper.Map<List<TeacherDTO>>(teachers);

            return teachersDTO;
        }

        public TeacherDTO GetTeacherById(int id, string includeProperities = null)
        {
            IQueryable<Teacher> query = dbSet;

            query = query.Where(c => c.Id == id);

            if (includeProperities != null)
            {
                query = HelperService<Teacher>.IncludeProps(includeProperities, query);
            }

            var teacher = query.FirstOrDefault();

            var teacherDTO = mapper.Map<TeacherDTO>(teacher);

            return teacherDTO;
        }

        public async Task<BaseResult> RemoveTeacher(int id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var teacher = _context.Teachers.Find(id);

                if (teacher == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find teacher";
                    return result;
                }

                _context.Teachers.Remove(teacher);
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

        public async Task<BaseResult> UpdateTeacher(UpdateTeacherDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var teacher = _context.Teachers.Find(Model.Id);

                if (teacher == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find teacher";
                    return result;
                }


                teacher.Name = Model.Name;
                teacher.Email = Model.Email;
                teacher.PhoneNumber = Model.PhoneNumber;
                teacher.DepartmentId = Model.DepartmentId;

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

        public List<TeacherDTO> GetAllTeachersByDepartmentId(int departmentId)
        {
            var teachers = _context.Teachers.Where(t => t.DepartmentId == departmentId).ToList();

            var teachersDTO = mapper.Map<List<TeacherDTO>>(teachers);

            return teachersDTO;
        }

    }
}