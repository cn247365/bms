using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="FavoriteMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>1/30/2021 9:49:58 PM </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(FavoriteMetadata))]
    public partial class Favorite
    {
    }

    public partial class FavoriteMetadata
    {
        [Display(Name = "Book",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.Favorite))]
        public Book Book { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.Favorite))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : Global Id")]
        [Display(Name = "GlobalId",Description ="Global Id",Prompt = "Global Id",ResourceType = typeof(resource.Favorite))]
        public int GlobalId { get; set; }

        [Required(ErrorMessage = "Please enter : Short Name")]
        [Display(Name = "ShortName",Description ="Short Name",Prompt = "Short Name",ResourceType = typeof(resource.Favorite))]
        [MaxLength(20)]
        public string ShortName { get; set; }

        [Display(Name = "DisplayName",Description ="Display Name",Prompt = "Display Name",ResourceType = typeof(resource.Favorite))]
        [MaxLength(56)]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Please enter : Book")]
        [Display(Name = "BookId",Description ="Book",Prompt = "Book",ResourceType = typeof(resource.Favorite))]
        public int BookId { get; set; }

        [Display(Name = "Title",Description ="Book Name",Prompt = "Book Name",ResourceType = typeof(resource.Favorite))]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter : Date")]
        [Display(Name = "AddDate",Description ="Date",Prompt = "Date",ResourceType = typeof(resource.Favorite))]
        public DateTime AddDate { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Favorite))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Favorite))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Favorite))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Favorite))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.Favorite))]
        public int TenantId { get; set; }

    }

}
