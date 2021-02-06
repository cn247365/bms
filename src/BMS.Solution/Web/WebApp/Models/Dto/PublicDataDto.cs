using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Dto
{
  public class PublicDataDto
  {
    public PublicDataDto()
    {
      this.Books = new HashSet<Book>();
      this.BooksPageData = new HashSet<Book>();
      this.Checkouts = new HashSet<CheckOut>();
      this.Stocks = new HashSet<Stock>();
    }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 6;
    public virtual IEnumerable<Book> Books { get; set; }
    public virtual IEnumerable<Book> BooksPageData { get; set; }
    public virtual IEnumerable<Stock> Stocks { get; set; }
    public virtual IEnumerable<CheckOut> Checkouts { get; set; }
  }
}