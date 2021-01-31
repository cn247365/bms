using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="BookMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>1/31/2021 9:30:44 AM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(BookMetadata))]
    public partial class Book
    {
    }

    public partial class BookMetadata
    {
        [Display(Name = "BookPictures",Description ="BookPictures",Prompt = "BookPictures",ResourceType = typeof(resource.Book))]
        public BookPicture BookPictures { get; set; }

        [Display(Name = "CheckOuts",Description ="CheckOuts",Prompt = "CheckOuts",ResourceType = typeof(resource.Book))]
        public CheckOut CheckOuts { get; set; }

        [Display(Name = "Comments",Description ="Comments",Prompt = "Comments",ResourceType = typeof(resource.Book))]
        public Comment Comments { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.Book))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : Book Name")]
        [Display(Name = "Title",Description ="Book Name",Prompt = "Book Name",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Title { get; set; }

        [Display(Name = "SubTitle",Description ="Sub Title",Prompt = "Sub Title",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string SubTitle { get; set; }

        [Display(Name = "Pic",Description ="Picture",Prompt = "Picture",ResourceType = typeof(resource.Book))]
        [MaxLength(512)]
        public string Pic { get; set; }

        [Display(Name = "LocalPic",Description ="Local Picture",Prompt = "Local Picture",ResourceType = typeof(resource.Book))]
        [MaxLength(512)]
        public string LocalPic { get; set; }

        [Display(Name = "Author",Description ="Author",Prompt = "Author",ResourceType = typeof(resource.Book))]
        [MaxLength(50)]
        public string Author { get; set; }

        [Display(Name = "Summary",Description ="Summary",Prompt = "Summary",ResourceType = typeof(resource.Book))]
        [MaxLength(4000)]
        public string Summary { get; set; }

        [Display(Name = "Publisher",Description ="Publisher",Prompt = "Publisher",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Publisher { get; set; }

        [Display(Name = "Pubplace",Description ="Pubplace",Prompt = "Pubplace",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Pubplace { get; set; }

        [Required(ErrorMessage = "Please enter : Page")]
        [Display(Name = "Page",Description ="Page",Prompt = "Page",ResourceType = typeof(resource.Book))]
        public int Page { get; set; }

        [Required(ErrorMessage = "Please enter : Price")]
        [Display(Name = "Price",Description ="Price",Prompt = "Price",ResourceType = typeof(resource.Book))]
        public decimal Price { get; set; }

        [Display(Name = "PurchasingPrice",Description ="PurchasingPrice",Prompt = "PurchasingPrice",ResourceType = typeof(resource.Book))]
        public decimal PurchasingPrice { get; set; }

        [Display(Name = "Binding",Description ="Binding",Prompt = "Binding",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Binding { get; set; }

        [Display(Name = "ISBN",Description ="ISBN",Prompt = "ISBN",ResourceType = typeof(resource.Book))]
        [MaxLength(20)]
        public string ISBN { get; set; }

        [Display(Name = "ISBN10",Description ="ISBN10",Prompt = "ISBN10",ResourceType = typeof(resource.Book))]
        [MaxLength(20)]
        public string ISBN10 { get; set; }

        [Display(Name = "Keyword",Description ="Keyword",Prompt = "Keyword",ResourceType = typeof(resource.Book))]
        [MaxLength(512)]
        public string Keyword { get; set; }

        [Display(Name = "Edition",Description ="Edition",Prompt = "Edition",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Edition { get; set; }

        [Display(Name = "Impression",Description ="Impression",Prompt = "Impression",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Impression { get; set; }

        [Display(Name = "Language",Description ="Language",Prompt = "Language",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Language { get; set; }

        [Display(Name = "Format",Description ="Format",Prompt = "Format",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Format { get; set; }

        [Display(Name = "Class",Description ="Class",Prompt = "Class",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string Class { get; set; }

        [Display(Name = "CIP",Description ="CIP",Prompt = "CIP",ResourceType = typeof(resource.Book))]
        [MaxLength(128)]
        public string CIP { get; set; }

        [Display(Name = "Reads",Description ="Reads",Prompt = "Reads",ResourceType = typeof(resource.Book))]
        public int Reads { get; set; }

        [Display(Name = "Popular",Description ="Popular",Prompt = "Popular",ResourceType = typeof(resource.Book))]
        public int Popular { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Book))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Book))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Book))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Book))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.Book))]
        public int TenantId { get; set; }

    }

}
