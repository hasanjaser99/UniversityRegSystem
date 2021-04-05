namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberAndDaysToSection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Number", c => c.Byte(nullable: false));
            AddColumn("dbo.Sections", "Days", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "Days");
            DropColumn("dbo.Sections", "Number");
        }
    }
}
