namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberOfHoursToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "NumberOfHours", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "NumberOfHours");
        }
    }
}
