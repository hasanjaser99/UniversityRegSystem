using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using UniversityRegSystem.Controllers;
using UniversityRegSystem.Services;
using UniversityRegSystem.Shared.InterfaceServices;

namespace UniversityRegSystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ICourseService, CourseService>();
            container.RegisterType<ITypeOfCourseService, TypeOfCourseService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<IFieldService, FieldService>();
            container.RegisterType<ITeacherService, TeacherService>();
            container.RegisterType<ISectionService, SectionService>();
            container.RegisterType<IStudentService, StudentService>();
            container.RegisterType<IRegisterationTimeService, RegisterationTimeService>();
            container.RegisterType<IFieldCourseService, FieldCourseService>();
            container.RegisterType<IStudentRegisterationTimeService, StudentRegisterationTimeService>();
            container.RegisterType<IRegisterationService, RegisterationService>();
            container.RegisterType<IStudentSectionsSerivce, StudentSectionsSerivce>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}