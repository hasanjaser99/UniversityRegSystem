namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDateFromDateTimeToStringInStudentRegisterationTimesModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentRegisterationTimes", "Date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentRegisterationTimes", "Date", c => c.DateTime(nullable: false));
        }
    }
}
