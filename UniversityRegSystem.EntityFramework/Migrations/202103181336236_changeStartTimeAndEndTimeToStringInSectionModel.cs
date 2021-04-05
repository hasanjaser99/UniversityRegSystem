namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeStartTimeAndEndTimeToStringInSectionModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sections", "StartTime", c => c.String());
            AlterColumn("dbo.Sections", "EndTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sections", "EndTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Sections", "StartTime", c => c.Time(nullable: false, precision: 7));
        }
    }
}
