using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
#nullable disable
namespace WebApp.Models
{
  public partial class Company : Entity
  {


    [Display(Name = "企业名称", Description = "企业名称")]
    [MaxLength(50)]
    [Required]
    [Index(IsUnique = true)]
    public string Name { get; set; }

    [Display(Name = "企业类型", Description = "企业类型")]
    [MaxLength(128)]
    public string Ctype { get; set; }

    [Display(Name = "地址", Description = "地址")]
    [MaxLength(128)]
    [DefaultValue("-")]
    public string Address { get; set; }

    [Display(Name = "联系人", Description = "联系人")]
    [MaxLength(56)]
    public string Contect { get; set; }
    [Display(Name = "联系电话", Description = "联系电话")]
    [MaxLength(56)]
    public string PhoneNumber { get; set; }
    [Display(Name = "注册日期", Description = "注册日期")]
    [DefaultValue("now")]
    public DateTime RegisterDate { get; set; }

    [Display(Name = "母公司", Description = "母公司")]
    public int? ParentId { get; set; }
    [ForeignKey("ParentId")]
    [Display(Name = "母公司", Description = "母公司")]
    public Company Parent { get; set; }
  }



}