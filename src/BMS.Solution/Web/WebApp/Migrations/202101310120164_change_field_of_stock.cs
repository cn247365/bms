namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_field_of_stock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckOuts", "BackQty", c => c.Int());
            AddColumn("dbo.Stocks", "Catetory", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "Catetory");
            DropColumn("dbo.CheckOuts", "BackQty");
        }
    }
}
