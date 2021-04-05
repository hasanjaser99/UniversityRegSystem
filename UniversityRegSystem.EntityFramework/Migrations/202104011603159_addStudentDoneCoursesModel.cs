namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStudentDoneCoursesModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentDoneCourses",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentDoneCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentDoneCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentDoneCourses", new[] { "CourseId" });
            DropIndex("dbo.StudentDoneCourses", new[] { "StudentId" });
            DropTable("dbo.StudentDoneCourses");
        }
    }
}
