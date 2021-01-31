using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="StockMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>1/31/2021 9:28:22 AM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(StockMetadata))]
    public partial class Stock
    {
    }

    public partial class StockMetadata
    {
        [Display(Name = "Book",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.Stock))]
        public Book Book { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.Stock))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : Book")]
        [Display(Name = "BookId",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.Stock))]
        public int BookId { get; set; }

        [Display(Name = "BarCode",Description ="BarCode",Prompt = "BarCode",ResourceType = typeof(resource.Stock))]
        [MaxLength(32)]
        public string BarCode { get; set; }

        [Display(Name = "ISBN",Description ="ISBN",Prompt = "ISBN",ResourceType = typeof(resource.Stock))]
        [MaxLength(10)]
        public string ISBN { get; set; }

        [Display(Name = "Title",Description ="Book Name",Prompt = "Book Name",ResourceType = typeof(resource.Stock))]
        [MaxLength(128)]
        public string Title { get; set; }

        [Display(Name = "Remark",Description ="Remark",Prompt = "Remark",ResourceType = typeof(resource.Stock))]
        [MaxLength(256)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : Qty")]
        [Display(Name = "Qty",Description ="Qty",Prompt = "Qty",ResourceType = typeof(resource.Stock))]
        public int Qty { get; set; }

        [Required(ErrorMessage = "Please enter : Price")]
        [Display(Name = "Price",Description ="Price",Prompt = "Price",ResourceType = typeof(resource.Stock))]
        public decimal Price { get; set; }

        [Display(Name = "PurchasingPrice",Description ="Purchasing Price",Prompt = "Purchasing Price",ResourceType = typeof(resource.Stock))]
        public decimal PurchasingPrice { get; set; }

        [Display(Name = "PurchaseDate",Description ="Purchase Date",Prompt = "Purchase Date",ResourceType = typeof(resource.Stock))]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "UserName",Description ="Manager",Prompt = "Manager",ResourceType = typeof(resource.Stock))]
        [MaxLength(32)]
        public string UserName { get; set; }

        [Display(Name = "Catetory",Description ="Catetory",Prompt = "Catetory",ResourceType = typeof(resource.Stock))]
        [MaxLength(128)]
        public string Catetory { get; set; }

        [Display(Name = "Location",Description ="Location",Prompt = "Location",ResourceType = typeof(resource.Stock))]
        [MaxLength(32)]
        public string Location { get; set; }

        [Display(Name = "Shelves",Description ="Shelves",Prompt = "Shelves",ResourceType = typeof(resource.Stock))]
        [MaxLength(32)]
        public string Shelves { get; set; }

        [Display(Name = "Tags",Description ="Tags",Prompt = "Tags",ResourceType = typeof(resource.Stock))]
        [MaxLength(128)]
        public string Tags { get; set; }

        [Display(Name = "Status",Description ="Status",Prompt = "Status",ResourceType = typeof(resource.Stock))]
        [MaxLength(12)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : Number Of Trades")]
        [Display(Name = "Trades",Description ="Number Of Trades",Prompt = "Number Of Trades",ResourceType = typeof(resource.Stock))]
        public int Trades { get; set; }

        [Required(ErrorMessage = "Please enter : Input Date")]
        [Display(Name = "InputDate",Description ="Input Date",Prompt = "Input Date",ResourceType = typeof(resource.Stock))]
        public DateTime InputDate { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Stock))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Stock))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Stock))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Stock))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.Stock))]
        public int TenantId { get; set; }

    }

}
