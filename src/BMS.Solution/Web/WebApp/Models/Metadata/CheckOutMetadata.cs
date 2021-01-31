using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CheckOutMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>1/30/2021 9:53:17 PM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CheckOutMetadata))]
    public partial class CheckOut
    {
    }

    public partial class CheckOutMetadata
    {
        [Display(Name = "Book",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.CheckOut))]
        public Book Book { get; set; }

        [Display(Name = "Employee",Description ="Employee",Prompt = "Employee",ResourceType = typeof(resource.CheckOut))]
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.CheckOut))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : Employee")]
        [Display(Name = "EmployeeId",Description ="Employee",Prompt = "Employee",ResourceType = typeof(resource.CheckOut))]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter : Global Id")]
        [Display(Name = "GlobalId",Description ="Global Id",Prompt = "Global Id",ResourceType = typeof(resource.CheckOut))]
        public int GlobalId { get; set; }

        [Required(ErrorMessage = "Please enter : Short Name")]
        [Display(Name = "ShortName",Description ="Short Name",Prompt = "Short Name",ResourceType = typeof(resource.CheckOut))]
        [MaxLength(20)]
        public string ShortName { get; set; }

        [Display(Name = "DisplayName",Description ="Display Name",Prompt = "Display Name",ResourceType = typeof(resource.CheckOut))]
        [MaxLength(56)]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Please enter : Book")]
        [Display(Name = "BookId",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.CheckOut))]
        public int BookId { get; set; }

        [Display(Name = "BarCode",Description ="BarCode",Prompt = "BarCode",ResourceType = typeof(resource.CheckOut))]
        [MaxLength(32)]
        public string BarCode { get; set; }

        [Display(Name = "ISBN",Description ="ISBN",Prompt = "ISBN",ResourceType = typeof(resource.CheckOut))]
        [MaxLength(10)]
        public string ISBN { get; set; }

        [Display(Name = "Title",Description ="Book Name",Prompt = "Book Name",ResourceType = typeof(resource.CheckOut))]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter : Qty")]
        [Display(Name = "Qty",Description ="Qty",Prompt = "Qty",ResourceType = typeof(resource.CheckOut))]
        public int Qty { get; set; }

        [Required(ErrorMessage = "Please enter : Borrow Date")]
        [Display(Name = "BorrowDate",Description ="Borrow Date",Prompt = "Borrow Date",ResourceType = typeof(resource.CheckOut))]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "BackDate",Description ="Back Date",Prompt = "Back Date",ResourceType = typeof(resource.CheckOut))]
        public DateTime BackDate { get; set; }

        [Display(Name = "ExpiryDate",Description ="Date of expiry",Prompt = "Date of expiry",ResourceType = typeof(resource.CheckOut))]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please enter : Days")]
        [Display(Name = "Days",Description ="Days",Prompt = "Days",ResourceType = typeof(resource.CheckOut))]
        public int Days { get; set; }

        [Display(Name = "Status",Description ="Status",Prompt = "Status",ResourceType = typeof(resource.CheckOut))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : Notified")]
        [Display(Name = "Notified",Description ="Notified",Prompt = "Notified",ResourceType = typeof(resource.CheckOut))]
        public bool Notified { get; set; }

        [Required(ErrorMessage = "Please enter : Expiry")]
        [Display(Name = "Expiry",Description ="Expiry",Prompt = "Expiry",ResourceType = typeof(resource.CheckOut))]
        public bool Expiry { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CheckOut))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CheckOut))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CheckOut))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CheckOut))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.CheckOut))]
        public int TenantId { get; set; }

    }

}
