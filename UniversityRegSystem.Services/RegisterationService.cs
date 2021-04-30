using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
    public class RegisterationService : IRegisterationService
    {
        private readonly ApplicationDbContext _context;

        public RegisterationService()
        {
            _context = new ApplicationDbContext();
        }

        public List<RegisterationSectionDTO> GetAllFieldSections(int fieldId)
        {
            var fieldIdParameter = new SqlParameter("@FieldId", fieldId);

            var registerationSectionsDTO = _context.Database
                .SqlQuery<RegisterationSectionDTO>("sp_fieldSections @FieldId", fieldIdParameter).ToList();

            return registerationSectionsDTO;
        }

        public async Task<BaseResult> RegisterCourse(string studentId, int courseId, int sectionId)
        {
            BaseResult response = new BaseResult();

            try
            {
                var studentDoneCourses = await _context.StudentDoneCourses
                    .Where(s => s.StudentId == studentId).ToListAsync();

                var doneCourse = studentDoneCourses.FirstOrDefault(d => d.CourseId == courseId);

                if (doneCourse != null)
                {
                    response.IsSucsess = false;
                    response.ErrorMessage = "You already finished this course";
                    return response;
                }

                var course = await _context.Courses.Where(c => c.Id == courseId)
                    .Include(c => c.PreviousCourse)
                    .FirstOrDefaultAsync();

                if (course.PreviousCourse != null)
                {
                    var prevDoneCourse = studentDoneCourses.FirstOrDefault(pd => pd.CourseId == course.PreviousCourse.Id);

                    if (prevDoneCourse == null)
                    {
                        response.IsSucsess = false;
                        response.ErrorMessage = "You need to take (" + course.PreviousCourse.Name + " Course) Before you register this course";
                        return response;
                    }
                }

                var studentSections = await _context.StudentSections
                    .Where(st => st.StudentId == studentId)
                    .Include(st => st.Section)
                    .Include(st => st.Section.Course)
                    .ToListAsync();

                var sectionCourse = studentSections.FirstOrDefault(sc => sc.Section.Course.Name == course.Name);

                if(sectionCourse != null)
                {
                    response.IsSucsess = false;
                    response.ErrorMessage = "You already registerd this course";
                    return response;
                }

                var section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == sectionId);

                if(section.NumberOfStudents == section.MaxNumberOfStudents)
                {
                    response.IsSucsess = false;
                    response.ErrorMessage = "Section Is Full of students , please try another section";
                    return response;
                }

                var sectionWithTheSameTime = studentSections
                    .FirstOrDefault(st => st.Section.StartTime == section.StartTime && st.Section.Days == section.Days);

                if (sectionWithTheSameTime != null)
                {
                    response.IsSucsess = false;
                    response.ErrorMessage = "You can't register two courses with the same time";
                    return response;
                }

                section.NumberOfStudents += 1;


                var studentSection = new StudentSections()
                {
                    SectionId = sectionId,
                    StudentId = studentId,
                };

                _context.StudentSections.Add(studentSection);

                await _context.SaveChangesAsync();

                response.IsSucsess = true;
                return response;

            }
            catch (Exception Ex)
            {
                response.IsSucsess = false;
                response.ErrorMessage = "Internal Server Error";
                return response;
            }
        }

        public async Task<BaseResult> DeleteCourse(string studentId, int sectionId)
        {
            BaseResult response = new BaseResult();

            try
            {
                var studentSection = await _context.StudentSections
                    .FirstOrDefaultAsync(st => st.SectionId == sectionId && st.StudentId == studentId);

                if(studentSection == null)
                {
                    response.IsSucsess = false;
                    response.ErrorMessage = "Section Not Found";
                    return response;
                }

                _context.StudentSections.Remove(studentSection);

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
    }
}