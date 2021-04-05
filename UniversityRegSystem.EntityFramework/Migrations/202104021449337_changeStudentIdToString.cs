namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeStudentIdToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentDoneCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentRegisterationTimes", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentSections", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentDoneCourses", new[] { "StudentId" });
            DropIndex("dbo.StudentRegisterationTimes", new[] { "StudentId" });
            DropIndex("dbo.StudentSections", new[] { "StudentId" });
            DropPrimaryKey("dbo.StudentDoneCourses");
            DropPrimaryKey("dbo.Students");
            DropPrimaryKey("dbo.StudentRegisterationTimes");
            DropPrimaryKey("dbo.StudentSections");
            AddColumn("dbo.Students", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StudentDoneCourses", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StudentRegisterationTimes", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StudentSections", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.StudentDoneCourses", new[] { "StudentId", "CourseId" });
            AddPrimaryKey("dbo.Students", "StudentId");
            AddPrimaryKey("dbo.StudentRegisterationTimes", new[] { "StudentId", "RegisterationTimeId" });
            AddPrimaryKey("dbo.StudentSections", new[] { "StudentId", "SectionId" });
            CreateIndex("dbo.StudentDoneCourses", "StudentId");
            CreateIndex("dbo.StudentRegisterationTimes", "StudentId");
            CreateIndex("dbo.StudentSections", "StudentId");
            AddForeignKey("dbo.StudentDoneCourses", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            AddForeignKey("dbo.StudentRegisterationTimes", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            AddForeignKey("dbo.StudentSections", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            DropColumn("dbo.Students", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.StudentSections", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentRegisterationTimes", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentDoneCourses", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentSections", new[] { "StudentId" });
            DropIndex("dbo.StudentRegisterationTimes", new[] { "StudentId" });
            DropIndex("dbo.StudentDoneCourses", new[] { "StudentId" });
            DropPrimaryKey("dbo.StudentSections");
            DropPrimaryKey("dbo.StudentRegisterationTimes");
            DropPrimaryKey("dbo.Students");
            DropPrimaryKey("dbo.StudentDoneCourses");
            AlterColumn("dbo.StudentSections", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentRegisterationTimes", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentDoneCourses", "StudentId", c => c.Int(nullable: false));
            DropColumn("dbo.Students", "StudentId");
            AddPrimaryKey("dbo.StudentSections", new[] { "StudentId", "SectionId" });
            AddPrimaryKey("dbo.StudentRegisterationTimes", new[] { "StudentId", "RegisterationTimeId" });
            AddPrimaryKey("dbo.Students", "Id");
            AddPrimaryKey("dbo.StudentDoneCourses", new[] { "StudentId", "CourseId" });
            CreateIndex("dbo.StudentSections", "StudentId");
            CreateIndex("dbo.StudentRegisterationTimes", "StudentId");
            CreateIndex("dbo.StudentDoneCourses", "StudentId");
            AddForeignKey("dbo.StudentSections", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            AddForeignKey("dbo.StudentRegisterationTimes", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            AddForeignKey("dbo.StudentDoneCourses", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
