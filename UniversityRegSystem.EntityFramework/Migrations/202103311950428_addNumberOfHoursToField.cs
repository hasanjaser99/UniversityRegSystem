namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberOfHoursToField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fields", "NumberOfHours", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fields", "NumberOfHours");
        }
    }
}
