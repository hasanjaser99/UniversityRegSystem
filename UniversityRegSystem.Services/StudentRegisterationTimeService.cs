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
using UniversityRegSystem.Shared.DTOS.StudentRegisterationTimeDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class StudentRegisterationTimeService : IStudentRegisterationTimeService
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<StudentRegisterationTimes> dbSet;

        private readonly IMapper mapper;

        public StudentRegisterationTimeService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
            this.dbSet = _context.Set<StudentRegisterationTimes>();
        }

        public async Task<BaseResult> CreateRegisterationTime(AddStudentRegisterationTimeDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var studentRegisterationTimes = new StudentRegisterationTimes()
                {
                    StudentId = Model.StudentId,
                    RegisterationTimeId = Model.RegisterationTimeId,
                    Date = DateTime.Now,
                };

                _context.StudentRegisterationTimes.Add(studentRegisterationTimes);

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

        public List<StudentRegisterationTimeDTO> GetAllRegisterationTimes(string studentId, string includeProperities = null)
        {
            IQueryable<StudentRegisterationTimes> query = dbSet;

            query = query.Where(s => s.StudentId == studentId);

            if (includeProperities != null)
            {
                query = HelperService<StudentRegisterationTimes>.IncludeProps(includeProperities, query);
            }

            var studentRegisterationTimes = query.ToList();

            var studentRegisterationTimesDTO = mapper.Map<List<StudentRegisterationTimeDTO>>(studentRegisterationTimes);

            return studentRegisterationTimesDTO;
        }

        public async Task<BaseResult> RemoveRegisterationTime(string studentId, int registerationId)
        {
            BaseResult result = new BaseResult();
            try
            {
                var studentRegisterationTime = _context.StudentRegisterationTimes
                    .FirstOrDefault(s => s.StudentId == studentId && s.RegisterationTimeId == registerationId);

                if (studentRegisterationTime == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find student Registeration Time";
                    return result;
                }

                _context.StudentRegisterationTimes.Remove(studentRegisterationTime);
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