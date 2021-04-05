namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTypeOfCoursefromCourse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "TypeOfCourseId", "dbo.TypeOfCourses");
            DropIndex("dbo.Courses", new[] { "TypeOfCourseId" });
            DropColumn("dbo.Courses", "TypeOfCourseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "TypeOfCourseId", c => c.Int());
            CreateIndex("dbo.Courses", "TypeOfCourseId");
            AddForeignKey("dbo.Courses", "TypeOfCourseId", "dbo.TypeOfCourses", "Id");
        }
    }
}
