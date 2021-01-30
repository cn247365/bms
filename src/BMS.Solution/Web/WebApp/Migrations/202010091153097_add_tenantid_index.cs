namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tenantid_index : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Attachments", "TenantId");
            CreateIndex("dbo.Categories", "TenantId");
            CreateIndex("dbo.Products", "TenantId");
            CreateIndex("dbo.CodeItems", "TenantId");
            CreateIndex("dbo.Companies", "TenantId");
            CreateIndex("dbo.Covid19", "TenantId");
            CreateIndex("dbo.DataTableImportMappings", "TenantId");
            CreateIndex("dbo.FaceLibs", "TenantId");
            CreateIndex("dbo.MenuItems", "TenantId");
            CreateIndex("dbo.Notifications", "TenantId");
            CreateIndex("dbo.OrderDetails", "TenantId");
            CreateIndex("dbo.Orders", "TenantId");
            CreateIndex("dbo.RoleMenus", "TenantId");
            CreateIndex("dbo.Works", "TenantId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Works", new[] { "TenantId" });
            DropIndex("dbo.RoleMenus", new[] { "TenantId" });
            DropIndex("dbo.Orders", new[] { "TenantId" });
            DropIndex("dbo.OrderDetails", new[] { "TenantId" });
            DropIndex("dbo.Notifications", new[] { "TenantId" });
            DropIndex("dbo.MenuItems", new[] { "TenantId" });
            DropIndex("dbo.FaceLibs", new[] { "TenantId" });
            DropIndex("dbo.DataTableImportMappings", new[] { "TenantId" });
            DropIndex("dbo.Covid19", new[] { "TenantId" });
            DropIndex("dbo.Companies", new[] { "TenantId" });
            DropIndex("dbo.CodeItems", new[] { "TenantId" });
            DropIndex("dbo.Products", new[] { "TenantId" });
            DropIndex("dbo.Categories", new[] { "TenantId" });
            DropIndex("dbo.Attachments", new[] { "TenantId" });
        }
    }
}
