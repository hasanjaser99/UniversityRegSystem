namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldCoursesModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FieldCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        TypeOfCourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Fields", t => t.FieldId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfCourses", t => t.TypeOfCourseId, cascadeDelete: true)
                .Index(t => t.FieldId)
                .Index(t => t.CourseId)
                .Index(t => t.TypeOfCourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FieldCourses", "TypeOfCourseId", "dbo.TypeOfCourses");
            DropForeignKey("dbo.FieldCourses", "FieldId", "dbo.Fields");
            DropForeignKey("dbo.FieldCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.FieldCourses", new[] { "TypeOfCourseId" });
            DropIndex("dbo.FieldCourses", new[] { "CourseId" });
            DropIndex("dbo.FieldCourses", new[] { "FieldId" });
            DropTable("dbo.FieldCourses");
        }
    }
}
