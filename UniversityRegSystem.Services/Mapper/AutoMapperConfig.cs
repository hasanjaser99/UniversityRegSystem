using AutoMapper;
using UniversityRegSystem.Models;
using UniversityRegSystem.Shared.DTOS.CourseDTOS;
using UniversityRegSystem.Shared.DTOS.DepartmentDTOS;
using UniversityRegSystem.Shared.DTOS.FieldCourseDTOS;
using UniversityRegSystem.Shared.DTOS.FieldDTOS;
using UniversityRegSystem.Shared.DTOS.RegisterationTimeDTOS;
using UniversityRegSystem.Shared.DTOS.SectionDTOS;
using UniversityRegSystem.Shared.DTOS.StudentDTOS;
using UniversityRegSystem.Shared.DTOS.StudentRegisterationTimeDTOS;
using UniversityRegSystem.Shared.DTOS.TeacherDTOS;
using UniversityRegSystem.Shared.DTOS.TypeOfCourseDTOS;

namespace UniversityRegSystem.Services.Mapper
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>().ReverseMap();
                cfg.CreateMap<Field, FieldDTO>().ReverseMap();
                cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
                cfg.CreateMap<TypeOfCourse, TypeOfCourseDTO>().ReverseMap();
                cfg.CreateMap<Teacher, TeacherDTO>().ReverseMap();
                cfg.CreateMap<Section, SectionDTO>().ReverseMap();
                cfg.CreateMap<Student, StudentDTO>().ReverseMap();
                cfg.CreateMap<RegisterationTime, RegisterationTimeDTO>().ReverseMap();
                cfg.CreateMap<FieldCourse, FieldCourseDTO>().ReverseMap();
                cfg.CreateMap<StudentRegisterationTimes, StudentRegisterationTimeDTO>().ReverseMap();
            });

            Mapper = config.CreateMapper();
        }
    }
}