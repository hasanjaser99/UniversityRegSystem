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
using UniversityRegSystem.Shared.DTOS.SectionDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class SectionService : ISectionService
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<Section> dbSet;

        private readonly IMapper mapper;


        public SectionService(ICourseService courseService)
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
            this.dbSet = _context.Set<Section>();
        }

        public async Task<BaseResult> CreateSection(AddSectionDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var sections = GetAllSectionsByCourseId((int)Model.CourseId);

                var sameTimeSections = sections
                    .FirstOrDefault(s => s.StartTime == Model.StartTime || s.EndTime == Model.EndTime);

                if(sameTimeSections != null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "There is another section that start or end in this time, please change the time";
                    return result;
                }

                byte maxSectionNumber = 0;

                if (sections.Count != 0)
                {
                    maxSectionNumber = sections.Max(m => m.Number);
                }

                var section = new Section()
                {
                    StartTime = Model.StartTime,
                    EndTime = Model.EndTime,
                    Days = Model.Days,
                    CourseId = Model.CourseId,
                    TeacherId = Model.TeacherId,
                    Number = ++maxSectionNumber,
                    MaxNumberOfStudents = Model.MaxNumberOfStudents
                };

                _context.Sections.Add(section);

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

        public List<SectionDTO> GetAllSections(string includeProperities = null)
        {
            IQueryable<Section> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<Section>.IncludeProps(includeProperities, query);
            }

            var sections = query.ToList();

            var sectionsDTO = mapper.Map<List<SectionDTO>>(sections);

            return sectionsDTO;
        }

        public SectionDTO GetSectionById(int id, string includeProperities = null)
        {
            IQueryable<Section> query = dbSet;

            query = query.Where(c => c.Id == id);

            if (includeProperities != null)
            {
                query = HelperService<Section>.IncludeProps(includeProperities, query);
            }

            var section = query.FirstOrDefault();

            var sectionDTO = mapper.Map<SectionDTO>(section);

            return sectionDTO;
        }

        public async Task<BaseResult> RemoveSection(int id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var section = _context.Sections.Find(id);

                if (section == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find section";
                    return result;
                }

                _context.Sections.Remove(section);
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

        public async Task<BaseResult> UpdateSection(UpdateSectionDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var section = _context.Sections.Find(Model.Id);

                if (section == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find section";
                    return result;
                }

                section.StartTime = Model.StartTime;
                section.EndTime = Model.EndTime;
                section.Days = Model.Days;
                section.MaxNumberOfStudents = Model.MaxNumberOfStudents;
                section.CourseId = Model.CourseId;
                section.TeacherId = Model.TeacherId;

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

        public List<SectionDTO> GetAllSectionsByCourseId(int courseId, string includeProperities = null)
        {
            IQueryable<Section> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<Section>.IncludeProps(includeProperities, query);
            }

            var sections = query.Where(s => s.CourseId == courseId).ToList();

            var sectionsDTO = mapper.Map<List<SectionDTO>>(sections);

            return sectionsDTO;
        }

    }
}