using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  public partial class Favorite:Entity
  {
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

    [Display(Name = "Book Name", Description = "Book Name")]
    [MaxLength(128)]
    public string Title { get; set; }
    [Display(Name = "Date", Description = "Date")]
    [DefaultValue("now")]
    public DateTime AddDate { get; set; }

  }
}