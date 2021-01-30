namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.Categories", new[] { "TenantId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "TenantId" });
            DropIndex("dbo.Covid19", new[] { "TenantId" });
            DropIndex("dbo.FaceLibs", new[] { "TenantId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "TenantId" });
            DropIndex("dbo.Orders", new[] { "OrderNo" });
            DropIndex("dbo.Orders", new[] { "TenantId" });
            DropIndex("dbo.Works", new[] { "TenantId" });
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Covid19");
            DropTable("dbo.FaceLibs");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Works");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Status = c.String(nullable: false, maxLength: 6),
                        Assignee = c.String(maxLength: 20),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        ToDoDateTime = c.DateTime(),
                        Enableed = c.Boolean(nullable: false),
                        Completed = c.Boolean(),
                        Hour = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(nullable: false, maxLength: 12),
                        Customer = c.String(nullable: false, maxLength: 30),
                        ShippingAddress = c.String(nullable: false, maxLength: 200),
                        Contect = c.String(maxLength: 30),
                        Remark = c.String(maxLength: 100),
                        OrderDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(maxLength: 30),
                        OrderId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FaceLibs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Description = c.String(),
                        ImgPath = c.String(),
                        ImgUrl = c.String(),
                        ImgRawData = c.Binary(),
                        ImgBase64Data = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Covid19",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        country = c.String(nullable: false, maxLength: 100),
                        province = c.String(maxLength: 100),
                        date = c.DateTime(nullable: false),
                        confirmed = c.Int(nullable: false),
                        deaths = c.Int(nullable: false),
                        recovered = c.Int(nullable: false),
                        latitude = c.Decimal(nullable: false, precision: 18, scale: 5),
                        longitude = c.Decimal(nullable: false, precision: 18, scale: 5),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Unit = c.String(maxLength: 10),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQty = c.Int(nullable: false),
                        IsRequiredQc = c.Boolean(nullable: false),
                        ConfirmDateTime = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Works", "TenantId");
            CreateIndex("dbo.Orders", "TenantId");
            CreateIndex("dbo.Orders", "OrderNo", unique: true);
            CreateIndex("dbo.OrderDetails", "TenantId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            CreateIndex("dbo.OrderDetails", "ProductId");
            CreateIndex("dbo.FaceLibs", "TenantId");
            CreateIndex("dbo.Covid19", "TenantId");
            CreateIndex("dbo.Products", "TenantId");
            CreateIndex("dbo.Products", "CategoryId");
            CreateIndex("dbo.Categories", "TenantId");
            AddForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
