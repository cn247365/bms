namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_index_of_ISBN10 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Books", new[] { "ISBN10" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Books", "ISBN10", unique: true);
        }
    }
}
