namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Picture = c.String(maxLength: 128),
                        Url = c.String(maxLength: 512),
                        Description = c.String(maxLength: 512),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        SubTitle = c.String(maxLength: 128),
                        Pic = c.String(maxLength: 512),
                        LocalPic = c.String(maxLength: 512),
                        Author = c.String(maxLength: 50),
                        Summary = c.String(),
                        Publisher = c.String(maxLength: 128),
                        Pubplace = c.String(maxLength: 128),
                        Page = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasingPrice = c.Decimal(precision: 18, scale: 2),
                        Binding = c.String(maxLength: 128),
                        ISBN = c.String(maxLength: 20),
                        ISBN10 = c.String(maxLength: 20),
                        Keyword = c.String(maxLength: 512),
                        Edition = c.String(maxLength: 128),
                        Impression = c.String(maxLength: 128),
                        Language = c.String(maxLength: 128),
                        Format = c.String(maxLength: 128),
                        Class = c.String(maxLength: 128),
                        CIP = c.String(maxLength: 128),
                        Reads = c.Int(),
                        Popular = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ISBN, unique: true)
                .Index(t => t.ISBN10, unique: true)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.CheckOuts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        GlobalId = c.Int(nullable: false),
                        ShortName = c.String(nullable: false, maxLength: 20),
                        DisplayName = c.String(maxLength: 56),
                        BookId = c.Int(nullable: false),
                        BarCode = c.String(maxLength: 32),
                        ISBN = c.String(maxLength: 10),
                        Title = c.String(maxLength: 128),
                        Qty = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        BackDate = c.DateTime(),
                        ExpiryDate = c.DateTime(),
                        Days = c.Int(nullable: false),
                        Status = c.String(maxLength: 20),
                        Notified = c.Boolean(nullable: false),
                        Expiry = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.BookId)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GlobalId = c.Int(nullable: false),
                        ShortName = c.String(nullable: false, maxLength: 20),
                        DisplayName = c.String(maxLength: 56),
                        CostCenter = c.String(maxLength: 10),
                        Department = c.String(maxLength: 32),
                        Company = c.String(maxLength: 128),
                        Email = c.String(maxLength: 56),
                        Phone = c.String(maxLength: 56),
                        Status = c.String(maxLength: 12),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 512),
                        UserName = c.String(maxLength: 20),
                        DisplayName = c.String(maxLength: 56),
                        PublishDate = c.DateTime(nullable: false),
                        Evaluate = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.BookId)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GlobalId = c.Int(nullable: false),
                        ShortName = c.String(nullable: false, maxLength: 20),
                        DisplayName = c.String(maxLength: 56),
                        BookId = c.Int(nullable: false),
                        Title = c.String(maxLength: 128),
                        AddDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        BarCode = c.String(maxLength: 32),
                        ISBN = c.String(maxLength: 10),
                        Title = c.String(maxLength: 128),
                        Remark = c.String(maxLength: 256),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasingPrice = c.Decimal(precision: 18, scale: 2),
                        PurchaseDate = c.DateTime(),
                        UserName = c.String(maxLength: 32),
                        Location = c.String(maxLength: 32),
                        Shelves = c.String(maxLength: 32),
                        Tags = c.String(maxLength: 128),
                        Status = c.String(maxLength: 12),
                        Trades = c.Int(nullable: false),
                        InputDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 20),
                        LastModifiedDate = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.TenantId);
            
            AddColumn("dbo.Notifications", "Confirmed", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "BookId", "dbo.Books");
            DropForeignKey("dbo.Favorites", "BookId", "dbo.Books");
            DropForeignKey("dbo.Comments", "ParentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "BookId", "dbo.Books");
            DropForeignKey("dbo.CheckOuts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.CheckOuts", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookPictures", "BookId", "dbo.Books");
            DropIndex("dbo.Stocks", new[] { "TenantId" });
            DropIndex("dbo.Stocks", new[] { "BookId" });
            DropIndex("dbo.Favorites", new[] { "TenantId" });
            DropIndex("dbo.Favorites", new[] { "BookId" });
            DropIndex("dbo.Comments", new[] { "TenantId" });
            DropIndex("dbo.Comments", new[] { "BookId" });
            DropIndex("dbo.Comments", new[] { "ParentId" });
            DropIndex("dbo.Employees", new[] { "TenantId" });
            DropIndex("dbo.CheckOuts", new[] { "TenantId" });
            DropIndex("dbo.CheckOuts", new[] { "BookId" });
            DropIndex("dbo.CheckOuts", new[] { "EmployeeId" });
            DropIndex("dbo.Books", new[] { "TenantId" });
            DropIndex("dbo.Books", new[] { "ISBN10" });
            DropIndex("dbo.Books", new[] { "ISBN" });
            DropIndex("dbo.BookPictures", new[] { "TenantId" });
            DropIndex("dbo.BookPictures", new[] { "BookId" });
            DropColumn("dbo.Notifications", "Confirmed");
            DropTable("dbo.Stocks");
            DropTable("dbo.Favorites");
            DropTable("dbo.Comments");
            DropTable("dbo.Employees");
            DropTable("dbo.CheckOuts");
            DropTable("dbo.Books");
            DropTable("dbo.BookPictures");
        }
    }
}
