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
using UniversityRegSystem.Shared.DTOS.FieldCourseDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class FieldCourseService : IFieldCourseService
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<FieldCourse> dbSet;

        private readonly IMapper mapper;

        public FieldCourseService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
            this.dbSet = _context.Set<FieldCourse>();
        }

        public async Task<BaseResult> CreateFieldCourse(AddFieldCourseDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var fieldCourse = new FieldCourse()
                {
                    CourseId = Model.CourseId,
                    FieldId = Model.FieldId,
                    TypeOfCourseId = Model.TypeOfCourseId
                };

                _context.FieldCourses.Add(fieldCourse);

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

        public List<FieldCourseDTO> GetAllFieldCourses(string includeProperities = null)
        {
            IQueryable<FieldCourse> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<FieldCourse>.IncludeProps(includeProperities, query);
            }

            var fieldCourses = query.ToList();

            var fieldCoursesDTO = mapper.Map<List<FieldCourseDTO>>(fieldCourses);

            return fieldCoursesDTO;
        }

        public FieldCourseDTO GetFieldCourseById(int id, string includeProperities = null)
        {
            IQueryable<FieldCourse> query = dbSet;

            query = query.Where(c => c.Id == id);

            if (includeProperities != null)
            {
                query = HelperService<FieldCourse>.IncludeProps(includeProperities, query);
            }

            var fieldCourse = query.FirstOrDefault();

            var fieldCourseDTO = mapper.Map<FieldCourseDTO>(fieldCourse);

            return fieldCourseDTO;
        }

        public async Task<BaseResult> RemoveFieldCourse(int id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var fieldCourse = _context.FieldCourses.Find(id);

                if (fieldCourse == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find field Course";
                    return result;
                }

                _context.FieldCourses.Remove(fieldCourse);
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

        public async Task<BaseResult> UpdateFieldCourse(UpdateFieldCourseDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var fieldCourse = _context.FieldCourses.Find(Model.Id);

                if (fieldCourse == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find field";
                    return result;
                }

                fieldCourse.FieldId = Model.FieldId;
                fieldCourse.CourseId = Model.CourseId;
                fieldCourse.TypeOfCourseId = Model.TypeOfCourseId;

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

        public List<FieldCourseDTO> GetFieldCoursesByFieldId(int fieldId, string includeProperities = null)
        {
            IQueryable<FieldCourse> query = dbSet;

            query = query.Where(c => c.FieldId == fieldId);

            if (includeProperities != null)
            {
                query = HelperService<FieldCourse>.IncludeProps(includeProperities, query);
            }

            var fieldCourses = query.ToList();

            var fieldCoursesDTO = mapper.Map<List<FieldCourseDTO>>(fieldCourses);

            return fieldCoursesDTO;
        }


    }
}