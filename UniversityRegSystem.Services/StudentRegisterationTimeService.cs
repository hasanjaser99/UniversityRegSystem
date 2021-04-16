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
using UniversityRegSystem.Shared.Utility;

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
                var studentRegisterationTime = _context.StudentRegisterationTimes
                    .FirstOrDefault(a => a.RegisterationTimeId == Model.RegisterationTimeId);

                if(studentRegisterationTime != null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "You can't Select registeration time twice";
                    return result;
                }


                studentRegisterationTime = new StudentRegisterationTimes()
                {
                    StudentId = Model.StudentId,
                    RegisterationTimeId = Model.RegisterationTimeId,
                    Date = DateTime.Now.ToString("dd/MM/yyyy"),
                };

                _context.StudentRegisterationTimes.Add(studentRegisterationTime);

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

            var currentDate = DateTime.Now.ToString("dd/MM/yyyy");

            query = query.Where(s => s.StudentId == studentId && s.Date == currentDate);

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

        public List<StudentRegisterationTimeDTO> GetAllRegisterationTimesByTime
            (string studentId, string includeProperities = null)
        {
            IQueryable<StudentRegisterationTimes> query = dbSet;

            var currentDate = DateTime.Now.ToString("dd/MM/yyyy");

            query = query.Where(s => s.StudentId == studentId && s.Date == currentDate);

            if (includeProperities != null)
            {
                query = HelperService<StudentRegisterationTimes>.IncludeProps(includeProperities, query);
            }

            var studentRegisterationTimes = query.ToList();

            var filteredRegisterationTimes = Enumerable.Empty<StudentRegisterationTimes>();

            foreach (var item in studentRegisterationTimes)
            {
                if (IsValidTime(item.RegisterationTime.StartTime, item.RegisterationTime.EndTime))
                {
                    filteredRegisterationTimes = filteredRegisterationTimes.Append(item);
                }
            }

            var studentRegisterationTimesDTO = mapper.Map<List<StudentRegisterationTimeDTO>>(filteredRegisterationTimes);

            return studentRegisterationTimesDTO;
        }

        //helpers 
        public bool IsValidTime(string startTime, string endTime)
        {
            var currentTime = DateTime.Now.ToString("hh:mm tt");

            if (currentTime[0] == '0')
            {
                currentTime = currentTime.Substring(1);
            }

            var currentTimeTT = TimeTT(currentTime);
            var startTimeTT = TimeTT(startTime);
            var endTimeTT = TimeTT(endTime);

            currentTime = StaticFunctions.RemoveTT(currentTime);
            startTime = StaticFunctions.RemoveTT(startTime);
            endTime = StaticFunctions.RemoveTT(endTime);

            var currentTimeArray = currentTime.Split(':');

            var startTimeArray = startTime.Split(':');

            var endTimeArray = endTime.Split(':');

            var transformedCurrentTime = double.Parse(currentTimeArray[0] + "." + currentTimeArray[1]);

            var transformedStartTime = double.Parse(startTimeArray[0] + "." + startTimeArray[1]);

            var transformedEndTime = double.Parse(endTimeArray[0] + "." + endTimeArray[1]);


            if (currentTimeTT == "PM" && startTimeTT == "PM" && (endTimeTT == "PM" || (endTimeTT == "AM" && endTimeArray[0] == "12")))
            {
                if(transformedCurrentTime > transformedStartTime && transformedCurrentTime < transformedEndTime)
                {
                    return true;
                }

                return false;
            }

            if (currentTimeTT == "AM" && startTimeTT == "AM" && endTimeTT == "AM")
            {
                if (transformedCurrentTime > transformedStartTime && transformedCurrentTime < transformedEndTime)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public string TimeTT(string time)
        {
            if(time.Contains("AM"))
            {
                return "AM";
            }

            return "PM";
        }

    }
}