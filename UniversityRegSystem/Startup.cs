using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using UniversityRegSystem.EntityFramework.Data;
using UniversityRegSystem.Shared.Utility;

[assembly: OwinStartupAttribute(typeof(UniversityRegSystem.Startup))]

namespace UniversityRegSystem
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        public void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            IdentityRole role;

            if (!roleManager.RoleExists(StaticData.Role_Admin))
            {
                role = new IdentityRole()
                {
                    Name = StaticData.Role_Admin
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(StaticData.Role_Student))
            {
                role = new IdentityRole()
                {
                    Name = StaticData.Role_Student
                };
                roleManager.Create(role);
            }

        }
    }
}