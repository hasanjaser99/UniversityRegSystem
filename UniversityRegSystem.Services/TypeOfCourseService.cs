using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegSystem.EntityFramework.Data;
using UniversityRegSystem.Models;
using UniversityRegSystem.Services.Mapper;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.TypeOfCourseDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class TypeOfCourseService : ITypeOfCourseService
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper mapper;

        public TypeOfCourseService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
        }

        public async Task<BaseResult> CreateTypeOfCourse(AddTypeOfCourseDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var typeOfCourse = new TypeOfCourse()
                {
                    Name = Model.Name,
                };

                _context.TypeOfCourses.Add(typeOfCourse);

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

        public List<TypeOfCourseDTO> GetAllTypeOfCourses()
        {
            var typeOfCourses = _context.TypeOfCourses.ToList();

            var typeOfCoursesDTO = mapper.Map<List<TypeOfCourseDTO>>(typeOfCourses);

            return typeOfCoursesDTO.ToList();
        }

        public TypeOfCourseDTO GetTypeOfCourseById(int id)
        {
            var typeOfCourse = _context.TypeOfCourses.Find(id);

            var typeOfCourseDTO = mapper.Map<TypeOfCourseDTO>(typeOfCourse);

            return typeOfCourseDTO;
        }

        public async Task<BaseResult> RemoveTypeOfCourse(int id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var typeOfCourse = _context.TypeOfCourses.Find(id);

                if (typeOfCourse == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find type of course";
                    return result;
                }

                _context.TypeOfCourses.Remove(typeOfCourse);
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

        public async Task<BaseResult> UpdateTypeOfCourse(UpdateTypeOfCourseDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var typeOfCourse = _context.TypeOfCourses.Find(Model.Id);

                if (typeOfCourse == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find type of course";
                    return result;
                }

                typeOfCourse.Name = Model.Name;

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