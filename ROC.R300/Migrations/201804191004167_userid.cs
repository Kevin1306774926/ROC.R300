namespace ROC.R300.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VisitInfoes", "UserId", c => c.String(maxLength: 50));
            AddColumn("dbo.VisitInfoes", "VisitTimes", c => c.Int(nullable: false));
            AddColumn("dbo.VisitInfoes", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.VisitInfoes", "LastTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.VisitInfoes", "TimeSpan", c => c.Int(nullable: false));
            DropColumn("dbo.VisitInfoes", "VisitTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VisitInfoes", "VisitTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.VisitInfoes", "TimeSpan");
            DropColumn("dbo.VisitInfoes", "LastTime");
            DropColumn("dbo.VisitInfoes", "CreateTime");
            DropColumn("dbo.VisitInfoes", "VisitTimes");
            DropColumn("dbo.VisitInfoes", "UserId");
        }
    }
}
