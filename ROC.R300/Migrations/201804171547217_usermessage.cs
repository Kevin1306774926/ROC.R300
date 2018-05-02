namespace ROC.R300.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usermessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Tel = c.String(maxLength: 20),
                        Message = c.String(maxLength: 2000),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserMessages");
        }
    }
}
