using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
  //员工
  public partial class Employee : Entity
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
    [Display(Name = "Cost Center", Description = "Cost Center")]
    [MaxLength(10)]
    public string CostCenter { get; set; }
    [Display(Name = "Department", Description = "Department")]
    [MaxLength(32)]
    public string Department { get; set; }
    [Display(Name = "Company", Description = "Company")]
    [MaxLength(128)]
    public string Company { get; set; }
    [Display(Name = "Email", Description = "Email")]
    [MaxLength(56)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Display(Name = "Phone", Description = "Phone")]
    [MaxLength(56)]
    public string Phone { get; set; }
    [Display(Name = "Status", Description = "Status")]
    [MaxLength(12)]
    public string Status { get; set; }

    public Employee()
    {
      this.CheckOuts = new HashSet<CheckOut>();
    }
    public ICollection<CheckOut> CheckOuts { get; set; }
  }
}