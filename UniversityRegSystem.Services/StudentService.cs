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
using UniversityRegSystem.Shared.DTOS.FieldDTOS;
using UniversityRegSystem.Shared.DTOS.StudentDTOS;
using UniversityRegSystem.Shared.InterfaceServices;
using UniversityRegSystem.Shared.Utility;

namespace UniversityRegSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<Student> dbSet;

        private readonly IMapper mapper;


        public StudentService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
            this.dbSet = _context.Set<Student>();
        }

        public async Task<BaseResult> CreateStudent(AddStudentDTO Model)
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

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
                    
                var result = await userManager.CreateAsync(userModel, Model.Password);

                    if (result.Errors.Count() > 0)
                    {
                        response.IsSucsess = false;
                        response.ErrorMessage = result.Errors.First();
                        return response;
                    }

                await userManager.AddToRoleAsync(userModel.Id, StaticData.Role_Student);

                var student = new Student()
                {
                    StudentId = userModel.Id,
                    Name = Model.Name,
                    Email = Model.Email,
                    PhoneNumber = Model.PhoneNumber,
                    FieldId = Model.FieldId
                };

                _context.Students.Add(student);

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

        public List<StudentDTO> GetAllStudents(string includeProperities = null)
        {
            IQueryable<Student> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<Student>.IncludeProps(includeProperities, query);
            }

            var students = query.ToList();

            var studentsDTO = mapper.Map<List<StudentDTO>>(students);

            return studentsDTO;
        }

        public StudentDTO GetStudentById(string id, string includeProperities = null)
        {
            IQueryable<Student> query = dbSet;

            query = query.Where(c => c.StudentId == id);

            if (includeProperities != null)
            {
                query = HelperService<Student>.IncludeProps(includeProperities, query);
            }

            var student = query.FirstOrDefault();

            var studentDTO = mapper.Map<StudentDTO>(student);

            return studentDTO;
        }

        public async Task<BaseResult> RemoveStudent(string id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var student = _context.Students.FirstOrDefault(s => s.StudentId == id);

                if (student == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find student";
                    return result;
                }

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

                var identityUser = userManager.FindByEmail(student.Email);

                await userManager.DeleteAsync(identityUser);

                _context.Students.Remove(student);
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

        public async Task<BaseResult> UpdateStudent(UpdateStudentDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var student = _context.Students.FirstOrDefault(s => s.StudentId == Model.StudentId);

                if (student == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find student";
                    return result;
                }

                if(student.Email != Model.Email || student.PhoneNumber != Model.PhoneNumber)
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

                    var identityUser = await userManager.FindByEmailAsync(student.Email);

                    identityUser.Email = Model.Email;
                    identityUser.PhoneNumber = Model.PhoneNumber;
                    identityUser.UserName = Model.Email;

                    await userManager.UpdateAsync(identityUser);
                }



                student.Name = Model.Name;
                student.Email = Model.Email;
                student.PhoneNumber = Model.PhoneNumber;
                student.FieldId = Model.FieldId;

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

        public FieldDTO GetStudentField(string studentId, string includeProperities = null)
        {
            IQueryable<Student> query = dbSet;

            query = query.Where(c => c.StudentId == studentId);

            if (includeProperities != null)
            {
                query = HelperService<Student>.IncludeProps(includeProperities, query);
            }

            var student = query.FirstOrDefault();

            var fieldDTO = mapper.Map<FieldDTO>(student.Field);

            return fieldDTO;
        }

    }
}