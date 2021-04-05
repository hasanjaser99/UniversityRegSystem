namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberOfStudentsToSection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "NumberOfStudents", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "NumberOfStudents");
        }
    }
}
