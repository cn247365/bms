using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="BookPictureMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>1/30/2021 9:48:46 PM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(BookPictureMetadata))]
    public partial class BookPicture
    {
    }

    public partial class BookPictureMetadata
    {
        [Display(Name = "Book",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.BookPicture))]
        public Book Book { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.BookPicture))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : Book")]
        [Display(Name = "BookId",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.BookPicture))]
        public int BookId { get; set; }

        [Display(Name = "Picture",Description ="Picture",Prompt = "Picture",ResourceType = typeof(resource.BookPicture))]
        [MaxLength(128)]
        public string Picture { get; set; }

        [Display(Name = "Url",Description ="Url",Prompt = "Url",ResourceType = typeof(resource.BookPicture))]
        [MaxLength(512)]
        public string Url { get; set; }

        [Display(Name = "Description",Description ="Description",Prompt = "Description",ResourceType = typeof(resource.BookPicture))]
        [MaxLength(512)]
        public string Description { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.BookPicture))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.BookPicture))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.BookPicture))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.BookPicture))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.BookPicture))]
        public int TenantId { get; set; }

    }

}
