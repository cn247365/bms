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
  //借书记录
  public partial class CheckOut : Entity
  {
    [Display(Name = "Employee", Description = "Employee")]
    public int EmployeeId { get; set; }
    [Display(Name = "Employee", Description = "Employee")]
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
    [Display(Name = "Global Id", Description = "Global Id")]
    [Required]
    public int GlobalId { get; set; }
    [Display(Name = "Short Name", Description = "Short Name")]
    [MaxLength(20)]
    [Required]
    public string ShortName { get; set; }
    [Display(Name = "Display Name", Description = "Display Name")]
    [MaxLength(56)]
    public string DisplayName { get; set; }

    [Display(Name = "Book", Description = "Book")]
    public int BookId { get; set; }
    [Display(Name = "Book", Description = "Book")]
    [ForeignKey("BookId")]
    public Book Book { get; set; }
    [Display(Name = "BarCode", Description = "BarCode")]
    [MaxLength(32)]
    public string BarCode { get; set; }
    [Display(Name = "ISBN", Description = "ISBN")]
    [MaxLength(30)]
    public string ISBN { get; set; }
    [Display(Name = "Book Name", Description = "Book Name")]
    [MaxLength(128)]
    public string Title { get; set; }
    [Display(Name = "Qty", Description = "Qty")]
    [DefaultValue(1)]
    public int Qty { get; set; }
    [Display(Name = "Qty", Description = "Qty")]
    [DefaultValue(null)]
    public int? BackQty { get; set; }
    [Display(Name = "Borrow Date", Description = "Borrow Date")]
    [DefaultValue("now")]
    public DateTime BorrowDate { get; set; }
    [Display(Name = "Back Date", Description = "Back Date")]
    [DefaultValue(null)]
    public DateTime? BackDate { get; set; }
    [Display(Name = "Date of expiry", Description = "Date of expiry")]
    public DateTime? ExpiryDate { get; set; }

    [Display(Name = "Days", Description = "Days")]
    [DefaultValue(1)]
    public int Days { get; set; }
    [Display(Name = "Status", Description = "Status")]
    [DefaultValue("Check Out")]
    [MaxLength(20)]
    public string Status { get; set; }
    [Display(Name = "Notified", Description = "Notified")]
    [DefaultValue(false)]
    public bool Notified { get; set; }
    [Display(Name = "Expiry", Description = "Expiry")]
    [DefaultValue(false)]
    public bool Expiry { get; set; }
  }
}