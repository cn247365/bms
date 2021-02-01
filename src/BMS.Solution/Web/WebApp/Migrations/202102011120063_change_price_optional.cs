namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_price_optional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Page", c => c.Int());
            AlterColumn("dbo.Books", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Stocks", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stocks", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Books", "Page", c => c.Int(nullable: false));
        }
    }
}
