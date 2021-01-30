using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public partial class BookPicture:Entity
    {
        [Display(Name = "Book", Description = "Book")]
        public int BookId { get; set; }
        [Display(Name = "Book", Description = "Book")]
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        [Display(Name = "Picture", Description = "Picture")]
        [MaxLength(128)]
        public string Picture { get; set; }
        [Display(Name = "Url", Description = "Url")]
        [MaxLength(512)]
        public string Url { get; set; }
        [Display(Name = "Description", Description = "Description")]
        [MaxLength(512)]
        public string Description { get; set; }

    }
}