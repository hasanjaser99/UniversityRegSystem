namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStudentRegisterationTimesModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentRegisterationTimes",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        RegisterationTimeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.RegisterationTimeId })
                .ForeignKey("dbo.RegisterationTimes", t => t.RegisterationTimeId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.RegisterationTimeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentRegisterationTimes", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentRegisterationTimes", "RegisterationTimeId", "dbo.RegisterationTimes");
            DropIndex("dbo.StudentRegisterationTimes", new[] { "RegisterationTimeId" });
            DropIndex("dbo.StudentRegisterationTimes", new[] { "StudentId" });
            DropTable("dbo.StudentRegisterationTimes");
        }
    }
}
