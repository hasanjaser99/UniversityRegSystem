using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegSystem.EntityFramework.Data;
using UniversityRegSystem.Models;
using UniversityRegSystem.Services.Mapper;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<Course> dbSet;

        private readonly IMapper mapper;

        public CourseService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
            this.dbSet = _context.Set<Course>();
        }

        public async Task<BaseResult> CreateCrouse(AddCourseDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var course = new Course()
                {
                    Name = Model.Name,
                    NumberOfHours = Model.NumberOfHours,
                    FieldId = Model.FieldId,
                    PreviousCourseId = Model.PreviousCourseId,
                };

                _context.Courses.Add(course);

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

        public List<CourseDTO> GetAllCourses(string includeProperities = null)
        {
            IQueryable<Course> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<Course>.IncludeProps(includeProperities, query);
            }

            var courses = query.ToList();

            var coursesDTO = mapper.Map<List<CourseDTO>>(courses);

            return coursesDTO.ToList();
        }

        public CourseDTO GetCourseById(int id, string includeProperities = null)
        {
            IQueryable<Course> query = dbSet;

            query = query.Where(c => c.Id == id);

            if (includeProperities != null)
            {
                query = HelperService<Course>.IncludeProps(includeProperities, query);
            }

            var course = query.FirstOrDefault();

            var courseDTO = mapper.Map<CourseDTO>(course);

            return courseDTO;
        }

        public async Task<BaseResult> RemoveCourse(int id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var course = _context.Courses.Find(id);

                if (course == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find course";
                    return result;
                }

                _context.Courses.Remove(course);
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

        public async Task<BaseResult> UpdateCourse(UpdateCourseDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var course = _context.Courses.Find(Model.Id);

                if (course == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find course";
                    return result;
                }

                course.Name = Model.Name;
                course.NumberOfHours = Model.NumberOfHours;
                course.FieldId = Model.FieldId;
                course.PreviousCourseId = Model.PreviousCourseId;

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

        public List<CourseDTO> GetAllCoursesByFieldId(int fieldId, string includeProperities = null)
        {
            IQueryable<Course> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<Course>.IncludeProps(includeProperities, query);
            }

            var courses = query.Where(c => c.FieldId == fieldId).ToList();

            var coursesDTO = mapper.Map<List<CourseDTO>>(courses);

            return coursesDTO;
        }

        public int? GetPreviousCourseId(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.PreviousCourseId == id);

            if (course == null) return null;

            return course.Id;
        }
    }
}