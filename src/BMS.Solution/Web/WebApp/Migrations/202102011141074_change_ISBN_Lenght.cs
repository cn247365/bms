namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_ISBN_Lenght : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Books", new[] { "ISBN" });
            AlterColumn("dbo.Books", "ISBN", c => c.String(maxLength: 30));
            AlterColumn("dbo.Books", "ISBN10", c => c.String(maxLength: 30));
            AlterColumn("dbo.CheckOuts", "ISBN", c => c.String(maxLength: 30));
            AlterColumn("dbo.Stocks", "ISBN", c => c.String(maxLength: 30));
            CreateIndex("dbo.Books", "ISBN", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Books", new[] { "ISBN" });
            AlterColumn("dbo.Stocks", "ISBN", c => c.String(maxLength: 10));
            AlterColumn("dbo.CheckOuts", "ISBN", c => c.String(maxLength: 10));
            AlterColumn("dbo.Books", "ISBN10", c => c.String(maxLength: 20));
            AlterColumn("dbo.Books", "ISBN", c => c.String(maxLength: 20));
            CreateIndex("dbo.Books", "ISBN", unique: true);
        }
    }
}
