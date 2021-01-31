namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_v1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stocks", "BarCode", c => c.String(nullable: false, maxLength: 32));
            CreateIndex("dbo.Stocks", "BarCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stocks", new[] { "BarCode" });
            AlterColumn("dbo.Stocks", "BarCode", c => c.String(maxLength: 32));
        }
    }
}
