namespace ROC.R300.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisitInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ip = c.String(maxLength: 20),
                        Browser = c.String(maxLength: 2000),
                        UrlReferrer = c.String(maxLength: 200),
                        VisitTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VisitInfoes");
        }
    }
}
