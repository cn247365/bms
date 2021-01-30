namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_removelogs : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Logs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MachineName = c.String(maxLength: 50),
                        Logged = c.DateTime(),
                        Level = c.String(maxLength: 5),
                        Message = c.String(),
                        Exception = c.String(),
                        ClientIP = c.String(),
                        Properties = c.String(),
                        User = c.String(maxLength: 50),
                        Logger = c.String(maxLength: 300),
                        Callsite = c.String(maxLength: 300),
                        Resolved = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
