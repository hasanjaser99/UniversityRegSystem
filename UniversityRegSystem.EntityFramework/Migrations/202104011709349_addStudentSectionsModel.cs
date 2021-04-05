namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStudentSectionsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentSections",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.SectionId })
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSections", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentSections", "SectionId", "dbo.Sections");
            DropIndex("dbo.StudentSections", new[] { "SectionId" });
            DropIndex("dbo.StudentSections", new[] { "StudentId" });
            DropTable("dbo.StudentSections");
        }
    }
}
