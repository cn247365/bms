namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class md_company_init : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Companies", new[] { "CreditCode" });
            DropColumn("dbo.Companies", "TradeCode");
            DropColumn("dbo.Companies", "MasterCustom");
            DropColumn("dbo.Companies", "CreditCode");
            DropColumn("dbo.Companies", "Code");
            DropColumn("dbo.Companies", "Scope");
            DropColumn("dbo.Companies", "LegalPerson");
            DropColumn("dbo.Companies", "ExpirationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "ExpirationDate", c => c.DateTime());
            AddColumn("dbo.Companies", "LegalPerson", c => c.String(maxLength: 56));
            AddColumn("dbo.Companies", "Scope", c => c.String(maxLength: 512));
            AddColumn("dbo.Companies", "Code", c => c.String(maxLength: 10));
            AddColumn("dbo.Companies", "CreditCode", c => c.String(maxLength: 18));
            AddColumn("dbo.Companies", "MasterCustom", c => c.String(maxLength: 10));
            AddColumn("dbo.Companies", "TradeCode", c => c.String(maxLength: 10));
            CreateIndex("dbo.Companies", "CreditCode", unique: true);
        }
    }
}
