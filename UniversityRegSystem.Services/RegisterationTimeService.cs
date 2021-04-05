using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegSystem.EntityFramework.Data;
using UniversityRegSystem.Models;
using UniversityRegSystem.Services.Mapper;
using UniversityRegSystem.Shared.DTOS;
using UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class RegisterationTimeService : IRegisterationTimeService
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper mapper;

        public RegisterationTimeService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
        }

        public async Task<BaseResult> CreateRegisterationTime(AddRegisterationTimeDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var registerationTime = new RegisterationTime()
                {
                    StartTime = Model.StartTime,
                    EndTime = Model.EndTime,
                    Day = Model.Day
                };

                _context.RegisterationTimes.Add(registerationTime);

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

        public List<RegisterationTimeDTO> GetAllRegisterationTimes()
        {
            var registerationTimes = _context.RegisterationTimes.ToList();

            var registerationTimesDTO = mapper.Map<List<RegisterationTimeDTO>>(registerationTimes);

            return registerationTimesDTO;
        }

        public RegisterationTimeDTO GetRegisterationTimeById(int id)
        {
            var registerationTime = _context.RegisterationTimes.Find(id);

            var registerationTimeDTO = mapper.Map<RegisterationTimeDTO>(registerationTime);

            return registerationTimeDTO;
        }

        public async Task<BaseResult> RemoveRegisterationTime(int id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var registerationTime = _context.RegisterationTimes.Find(id);

                if (registerationTime == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find type of course";
                    return result;
                }

                _context.RegisterationTimes.Remove(registerationTime);
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

        public async Task<BaseResult> UpdateRegisterationTime(UpdateRegisterationTimeDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var registerationTime = _context.RegisterationTimes.Find(Model.Id);

                if (registerationTime == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find type of course";
                    return result;
                }

                registerationTime.StartTime = Model.StartTime;
                registerationTime.EndTime = Model.EndTime;
                registerationTime.Day = Model.Day;

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

        public List<RegisterationTimeDTO> GetRegisterationTimesByDay()
        {
            var day = DateTime.Now.ToString("dddd");

            var registerationTimes = _context.RegisterationTimes.Where(r => r.Day == day).ToList();

            var registerationTimesDTO = mapper.Map<List<RegisterationTimeDTO>>(registerationTimes);

            return registerationTimesDTO;
        }

    }
}