namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_phone_field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckOuts", "Phone", c => c.String(maxLength: 56));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CheckOuts", "Phone");
        }
    }
}
