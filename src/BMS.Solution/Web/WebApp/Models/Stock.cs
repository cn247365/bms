using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
  //图书库存
  public partial class Stock : Entity
  {
    [Display(Name = "Book", Description = "Book")]
    public int BookId { get; set; }
    [Display(Name = "Book", Description = "Book")]
    [ForeignKey("BookId")]
    public Book Book { get; set; }
    [Display(Name = "BarCode", Description = "BarCode")]
    [MaxLength(32)]
    [Required]
    [Index(IsUnique =true)]
    public string BarCode { get; set; }
    [Display(Name = "ISBN", Description = "ISBN")]
    [MaxLength(10)]
    public string ISBN { get; set; }
    [Display(Name = "Book Name", Description = "Book Name")]
    [MaxLength(128)]
    public string Title { get; set; }
    [Display(Name = "Remark", Description = "Remark")]
    [MaxLength(256)]
    public string Remark { get; set; }
    [Display(Name = "Qty", Description = "Qty")]
    public int Qty { get; set; }
    [Display(Name = "Price", Description = "Price")]
    public decimal Price { get; set; }
    [Display(Name = "Purchasing Price", Description = "Purchasing Price")]
    public decimal? PurchasingPrice { get; set; }
    [Display(Name = "Purchase Date", Description = "Purchase Date")]
    [DefaultValue(null)]
    public DateTime? PurchaseDate { get; set; }
    [Display(Name = "Manager", Description = "Manager")]
    [MaxLength(32)]
    public string UserName { get; set; }
    [Display(Name = "Catetory", Description = "Catetory")]
    [MaxLength(128)]
    public string Catetory { get; set; }
    [Display(Name = "Location", Description = "Location")]
    [MaxLength(32)]
    public string Location { get; set; }
    [Display(Name = "Shelves", Description = "Shelves")]
    [MaxLength(32)]
    public string Shelves { get; set; }
    [Display(Name = "Tags", Description = "Tags")]
    [MaxLength(128)]
    public string Tags { get; set; }
    [Display(Name = "Status", Description = "Status")]
    [MaxLength(12)]
    public string Status { get; set; }
    [Display(Name = "Number Of Trades", Description = "Number Of Trades")]
    public int Trades { get; set; }
    [Display(Name = "Input Date", Description = "Input Date")]
    [DefaultValue("now")]
    public DateTime InputDate { get; set; }



  }
}