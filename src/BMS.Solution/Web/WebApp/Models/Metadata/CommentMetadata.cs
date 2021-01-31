using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CommentMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>1/30/2021 9:47:27 PM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CommentMetadata))]
    public partial class Comment
    {
    }

    public partial class CommentMetadata
    {
        [Display(Name = "Book",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.Comment))]
        public Book Book { get; set; }

        [Display(Name = "Parent",Description ="Comment",Prompt = "Comment",ResourceType = typeof(resource.Comment))]
        public Comment Parent { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.Comment))]
        public int Id { get; set; }

        [Display(Name = "Content",Description ="Content",Prompt = "Content",ResourceType = typeof(resource.Comment))]
        [MaxLength(512)]
        public string Content { get; set; }

        [Display(Name = "UserName",Description ="User Name",Prompt = "User Name",ResourceType = typeof(resource.Comment))]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Display(Name = "DisplayName",Description ="Display Name",Prompt = "Display Name",ResourceType = typeof(resource.Comment))]
        [MaxLength(56)]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Please enter : Date")]
        [Display(Name = "PublishDate",Description ="Date",Prompt = "Date",ResourceType = typeof(resource.Comment))]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Please enter : Evaluate")]
        [Display(Name = "Evaluate",Description ="Evaluate",Prompt = "Evaluate",ResourceType = typeof(resource.Comment))]
        public int Evaluate { get; set; }

        [Required(ErrorMessage = "Please enter : Comment")]
        [Display(Name = "ParentId",Description ="Comment",Prompt = "Comment",ResourceType = typeof(resource.Comment))]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Please enter : Book")]
        [Display(Name = "BookId",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.Comment))]
        public int BookId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Comment))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Comment))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Comment))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Comment))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.Comment))]
        public int TenantId { get; set; }

    }

}
