using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Dto
{
  public class BorrowInputDto
  {
    public int GlobalId { get; set; }
    public string Phone { get; set; }
    public string BarCode { get; set; }
    public string Title { get; set; }
  }

  public class RetuningInputDto
  {
    public int GlobalId { get; set; }
    public string DisplayName { get; set; }
    public string BarCode { get; set; }
    public string Title { get; set; }
  }
}