using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using UniversityRegSystem.Models;

namespace UniversityRegSystem.EntityFramework.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TypeOfCourse> TypeOfCourses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<RegisterationTime> RegisterationTimes { get; set; }
        public DbSet<FieldCourse> FieldCourses { get; set; }
        public DbSet<StudentDoneCourses> StudentDoneCourses { get; set; }
        public DbSet<StudentRegisterationTimes> StudentRegisterationTimes { get; set; }
        public DbSet<StudentSections> StudentSections { get; set; }
    }
}