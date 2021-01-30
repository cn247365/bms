namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _initdatabase : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Attachments",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FileName = c.String(nullable: false, maxLength: 100),
            //            Size = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            FileId = c.String(maxLength: 100),
            //            Ext = c.String(maxLength: 100),
            //            Tags = c.String(),
            //            FilePath = c.String(),
            //            RelativePath = c.String(),
            //            RefKey = c.String(maxLength: 100),
            //            Owner = c.String(maxLength: 20),
            //            Upload = c.DateTime(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Categories",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 30),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Products",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            Unit = c.String(maxLength: 10),
            //            UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            StockQty = c.Int(nullable: false),
            //            IsRequiredQc = c.Boolean(nullable: false),
            //            ConfirmDateTime = c.DateTime(nullable: false),
            //            CategoryId = c.Int(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
            //    .Index(t => t.CategoryId);
            
            //CreateTable(
            //    "dbo.CodeItems",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Code = c.String(nullable: false, maxLength: 50),
            //            Text = c.String(nullable: false, maxLength: 50),
            //            CodeType = c.String(nullable: false, maxLength: 20),
            //            Description = c.String(nullable: false, maxLength: 80),
            //            IsDisabled = c.Int(nullable: false),
            //            Multiple = c.Boolean(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => new { t.Code, t.CodeType }, unique: true, name: "CodeItem_IX");
            
            //CreateTable(
            //    "dbo.Companies",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            Code = c.String(nullable: false, maxLength: 12),
            //            Address = c.String(maxLength: 50),
            //            Contect = c.String(maxLength: 12),
            //            PhoneNumber = c.String(maxLength: 20),
            //            RegisterDate = c.DateTime(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Name, unique: true)
            //    .Index(t => t.Code, unique: true);
            
            //CreateTable(
            //    "dbo.Covid19",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            country = c.String(nullable: false, maxLength: 100),
            //            province = c.String(maxLength: 100),
            //            date = c.DateTime(nullable: false),
            //            confirmed = c.Int(nullable: false),
            //            deaths = c.Int(nullable: false),
            //            recovered = c.Int(nullable: false),
            //            latitude = c.Decimal(nullable: false, precision: 18, scale: 5),
            //            longitude = c.Decimal(nullable: false, precision: 18, scale: 5),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.DataTableImportMappings",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EntitySetName = c.String(nullable: false, maxLength: 50),
            //            FieldName = c.String(nullable: false, maxLength: 50),
            //            DefaultValue = c.String(maxLength: 50),
            //            TypeName = c.String(maxLength: 30),
            //            IsRequired = c.Boolean(nullable: false),
            //            SourceFieldName = c.String(maxLength: 50),
            //            IsEnabled = c.Boolean(nullable: false),
            //            IgnoredColumn = c.Boolean(nullable: false),
            //            RegularExpression = c.String(maxLength: 100),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => new { t.EntitySetName, t.FieldName }, unique: true, name: "IX_DataTableImportMapping");
            
            //CreateTable(
            //    "dbo.FaceLibs",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Label = c.String(),
            //            Description = c.String(),
            //            ImgPath = c.String(),
            //            ImgUrl = c.String(),
            //            ImgRawData = c.Binary(),
            //            ImgBase64Data = c.String(),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Logs",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            MachineName = c.String(maxLength: 50),
            //            Logged = c.DateTime(),
            //            Level = c.String(maxLength: 5),
            //            Message = c.String(),
            //            Exception = c.String(),
            //            ClientIP = c.String(),
            //            Properties = c.String(),
            //            User = c.String(maxLength: 50),
            //            Logger = c.String(maxLength: 300),
            //            Callsite = c.String(maxLength: 300),
            //            Resolved = c.Boolean(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.MenuItems",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(nullable: false, maxLength: 50),
            //            Description = c.String(maxLength: 100),
            //            Code = c.String(nullable: false, maxLength: 20),
            //            Url = c.String(nullable: false, maxLength: 100),
            //            Controller = c.String(maxLength: 100),
            //            Action = c.String(maxLength: 100),
            //            IconCls = c.String(maxLength: 50),
            //            IsEnabled = c.Boolean(nullable: false),
            //            ParentId = c.Int(),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.MenuItems", t => t.ParentId)
            //    .Index(t => t.ParentId);
            
            //CreateTable(
            //    "dbo.Notifications",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(nullable: false, maxLength: 50),
            //            Content = c.String(maxLength: 255),
            //            Link = c.String(maxLength: 255),
            //            Read = c.Boolean(nullable: false),
            //            From = c.String(),
            //            To = c.String(),
            //            Group = c.Int(nullable: false),
            //            Created = c.DateTime(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.OrderDetails",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ProductId = c.Int(nullable: false),
            //            Qty = c.Int(nullable: false),
            //            Price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Remark = c.String(maxLength: 30),
            //            OrderId = c.Int(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
            //    .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
            //    .Index(t => t.ProductId)
            //    .Index(t => t.OrderId);
            
            //CreateTable(
            //    "dbo.Orders",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            OrderNo = c.String(nullable: false, maxLength: 12),
            //            Customer = c.String(nullable: false, maxLength: 30),
            //            ShippingAddress = c.String(nullable: false, maxLength: 200),
            //            Contect = c.String(maxLength: 30),
            //            Remark = c.String(maxLength: 100),
            //            OrderDate = c.DateTime(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.OrderNo, unique: true);
            
            //CreateTable(
            //    "dbo.RoleMenus",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            RoleName = c.String(nullable: false, maxLength: 20),
            //            MenuId = c.Int(nullable: false),
            //            IsEnabled = c.Boolean(nullable: false),
            //            Create = c.Boolean(nullable: false),
            //            Edit = c.Boolean(nullable: false),
            //            Delete = c.Boolean(nullable: false),
            //            Import = c.Boolean(nullable: false),
            //            Export = c.Boolean(nullable: false),
            //            FunctionPoint1 = c.Boolean(nullable: false),
            //            FunctionPoint2 = c.Boolean(nullable: false),
            //            FunctionPoint3 = c.Boolean(nullable: false),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.MenuItems", t => t.MenuId, cascadeDelete: true)
            //    .Index(t => new { t.RoleName, t.MenuId }, unique: true, name: "IX_rolemenu");
            
            //CreateTable(
            //    "dbo.Works",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 20),
            //            Status = c.String(nullable: false, maxLength: 6),
            //            Assignee = c.String(maxLength: 20),
            //            StartDate = c.DateTime(),
            //            EndDate = c.DateTime(),
            //            ToDoDateTime = c.DateTime(),
            //            Enableed = c.Boolean(nullable: false),
            //            Completed = c.Boolean(),
            //            Hour = c.Int(nullable: false),
            //            Priority = c.Int(nullable: false),
            //            Score = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(maxLength: 20),
            //            LastModifiedDate = c.DateTime(),
            //            LastModifiedBy = c.String(maxLength: 20),
            //            TenantId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.RoleMenus", "MenuId", "dbo.MenuItems");
            //DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            //DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            //DropForeignKey("dbo.MenuItems", "ParentId", "dbo.MenuItems");
            //DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            //DropIndex("dbo.RoleMenus", "IX_rolemenu");
            //DropIndex("dbo.Orders", new[] { "OrderNo" });
            //DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            //DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            //DropIndex("dbo.MenuItems", new[] { "ParentId" });
            //DropIndex("dbo.DataTableImportMappings", "IX_DataTableImportMapping");
            //DropIndex("dbo.Companies", new[] { "Code" });
            //DropIndex("dbo.Companies", new[] { "Name" });
            //DropIndex("dbo.CodeItems", "CodeItem_IX");
            //DropIndex("dbo.Products", new[] { "CategoryId" });
            //DropTable("dbo.Works");
            //DropTable("dbo.RoleMenus");
            //DropTable("dbo.Orders");
            //DropTable("dbo.OrderDetails");
            //DropTable("dbo.Notifications");
            //DropTable("dbo.MenuItems");
            //DropTable("dbo.Logs");
            //DropTable("dbo.FaceLibs");
            //DropTable("dbo.DataTableImportMappings");
            //DropTable("dbo.Covid19");
            //DropTable("dbo.Companies");
            //DropTable("dbo.CodeItems");
            //DropTable("dbo.Products");
            //DropTable("dbo.Categories");
            //DropTable("dbo.Attachments");
        }
    }
}
