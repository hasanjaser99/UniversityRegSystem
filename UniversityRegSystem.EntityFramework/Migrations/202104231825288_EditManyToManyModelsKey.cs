namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditManyToManyModelsKey : DbMigration
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
            DropPrimaryKey("dbo.StudentRegisterationTimes");
            DropPrimaryKey("dbo.StudentSections");
            AddColumn("dbo.StudentDoneCourses", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.StudentRegisterationTimes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.StudentSections", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.StudentDoneCourses", "StudentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.StudentRegisterationTimes", "StudentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.StudentSections", "StudentId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.StudentDoneCourses", "Id");
            AddPrimaryKey("dbo.StudentRegisterationTimes", "Id");
            AddPrimaryKey("dbo.StudentSections", "Id");
            CreateIndex("dbo.StudentDoneCourses", "StudentId");
            CreateIndex("dbo.StudentRegisterationTimes", "StudentId");
            CreateIndex("dbo.StudentSections", "StudentId");
            AddForeignKey("dbo.StudentDoneCourses", "StudentId", "dbo.Students", "StudentId");
            AddForeignKey("dbo.StudentRegisterationTimes", "StudentId", "dbo.Students", "StudentId");
            AddForeignKey("dbo.StudentSections", "StudentId", "dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSections", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentRegisterationTimes", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentDoneCourses", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentSections", new[] { "StudentId" });
            DropIndex("dbo.StudentRegisterationTimes", new[] { "StudentId" });
            DropIndex("dbo.StudentDoneCourses", new[] { "StudentId" });
            DropPrimaryKey("dbo.StudentSections");
            DropPrimaryKey("dbo.StudentRegisterationTimes");
            DropPrimaryKey("dbo.StudentDoneCourses");
            AlterColumn("dbo.StudentSections", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StudentRegisterationTimes", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StudentDoneCourses", "StudentId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.StudentSections", "Id");
            DropColumn("dbo.StudentRegisterationTimes", "Id");
            DropColumn("dbo.StudentDoneCourses", "Id");
            AddPrimaryKey("dbo.StudentSections", new[] { "StudentId", "SectionId" });
            AddPrimaryKey("dbo.StudentRegisterationTimes", new[] { "StudentId", "RegisterationTimeId" });
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
