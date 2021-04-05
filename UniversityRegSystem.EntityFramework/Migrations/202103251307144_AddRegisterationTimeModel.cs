namespace UniversityRegSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegisterationTimeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisterationTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.String(),
                        EndTime = c.String(),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegisterationTimes");
        }
    }
}
