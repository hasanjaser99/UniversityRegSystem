namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMaxNumberOfStudentsToSectionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "MaxNumberOfStudents", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "MaxNumberOfStudents");
        }
    }
}
