namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_changecompany : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Companies", new[] { "Code" });
            AddColumn("dbo.Companies", "TradeCode", c => c.String(maxLength: 10));
            AddColumn("dbo.Companies", "MasterCustom", c => c.String(maxLength: 10));
            AddColumn("dbo.Companies", "CreditCode", c => c.String(maxLength: 18));
            AddColumn("dbo.Companies", "Ctype", c => c.String(maxLength: 128));
            AddColumn("dbo.Companies", "Scope", c => c.String(maxLength: 512));
            AddColumn("dbo.Companies", "LegalPerson", c => c.String(maxLength: 56));
            AddColumn("dbo.Companies", "ExpirationDate", c => c.DateTime());
            AddColumn("dbo.Companies", "ParentId", c => c.Int());
            AlterColumn("dbo.Companies", "Code", c => c.String(maxLength: 10));
            AlterColumn("dbo.Companies", "Address", c => c.String(maxLength: 128));
            AlterColumn("dbo.Companies", "Contect", c => c.String(maxLength: 56));
            AlterColumn("dbo.Companies", "PhoneNumber", c => c.String(maxLength: 56));
            CreateIndex("dbo.Companies", "CreditCode", unique: true);
            CreateIndex("dbo.Companies", "ParentId");
            AddForeignKey("dbo.Companies", "ParentId", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "ParentId", "dbo.Companies");
            DropIndex("dbo.Companies", new[] { "ParentId" });
            DropIndex("dbo.Companies", new[] { "CreditCode" });
            AlterColumn("dbo.Companies", "PhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Companies", "Contect", c => c.String(maxLength: 12));
            AlterColumn("dbo.Companies", "Address", c => c.String(maxLength: 50));
            AlterColumn("dbo.Companies", "Code", c => c.String(nullable: false, maxLength: 12));
            DropColumn("dbo.Companies", "ParentId");
            DropColumn("dbo.Companies", "ExpirationDate");
            DropColumn("dbo.Companies", "LegalPerson");
            DropColumn("dbo.Companies", "Scope");
            DropColumn("dbo.Companies", "Ctype");
            DropColumn("dbo.Companies", "CreditCode");
            DropColumn("dbo.Companies", "MasterCustom");
            DropColumn("dbo.Companies", "TradeCode");
            CreateIndex("dbo.Companies", "Code", unique: true);
        }
    }
}
