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
using UniversityRegSystem.Shared.DTOS.FieldDTOS;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem.Services
{
    public class FieldService : IFieldService
    {
        private readonly ApplicationDbContext _context;

        internal DbSet<Field> dbSet;

        private readonly IMapper mapper;

        public FieldService()
        {
            _context = new ApplicationDbContext();
            mapper = AutoMapperConfig.Mapper;
            this.dbSet = _context.Set<Field>();
        }

        public async Task<BaseResult> CreateField(AddFieldDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var field = new Field()
                {
                    Name = Model.Name,
                    NumberOfHours = Model.NumberOfHours,
                    DepartmentId = Model.DepartmentId,
                };

                _context.Fields.Add(field);

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

        public List<FieldDTO> GetAllFields(string includeProperities = null)
        {
            IQueryable<Field> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<Field>.IncludeProps(includeProperities, query);
            }

            var fields = query.ToList();

            var fieldsDTO = mapper.Map<List<FieldDTO>>(fields);

            return fieldsDTO;
        }

        public FieldDTO GetFieldById(int id, string includeProperities = null)
        {
            IQueryable<Field> query = dbSet;

            query = query.Where(c => c.Id == id);

            if (includeProperities != null)
            {
                query = HelperService<Field>.IncludeProps(includeProperities, query);
            }

            var field = query.FirstOrDefault();

            var fieldDTO = mapper.Map<FieldDTO>(field);

            return fieldDTO;
        }

        public async Task<BaseResult> RemoveField(int id)
        {
            BaseResult result = new BaseResult();
            try
            {
                var field = _context.Fields.Find(id);

                if (field == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find field";
                    return result;
                }

                _context.Fields.Remove(field);
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

        public async Task<BaseResult> UpdateField(UpdateFieldDTO Model)
        {
            BaseResult result = new BaseResult();
            try
            {
                var field = _context.Fields.Find(Model.Id);

                if (field == null)
                {
                    result.IsSucsess = false;
                    result.ErrorMessage = "Could not find field";
                    return result;
                }

                field.Name = Model.Name;
                field.NumberOfHours = Model.NumberOfHours;
                field.DepartmentId = Model.DepartmentId;

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


        public List<FieldDTO> GetAllFieldsByDepartmentId(int departmentId, string includeProperities = null)
        {
            IQueryable<Field> query = dbSet;

            if (includeProperities != null)
            {
                query = HelperService<Field>.IncludeProps(includeProperities, query);
            }

            var fields = query.Where(f => f.DepartmentId == departmentId).ToList();

            var fieldsDTO = mapper.Map<List<FieldDTO>>(fields);

            return fieldsDTO;
        }
    }
}