using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public partial class Book:Entity
    {
        [Display(Name = "Book Name", Description = "Book Name")]
        [MaxLength(128)]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Sub Title", Description = "Sub Title")]
        [MaxLength(128)]
        public string SubTitle { get; set; }
        [Display(Name = "Picture", Description = "Picture")]
        [MaxLength(512)]
        public string Pic { get; set; }
        [Display(Name = "Local Picture", Description = "Local Picture")]
        [MaxLength(512)]
        public string LocalPic { get; set; }
        [Display(Name = "Author", Description = "Author")]
        [MaxLength(50)]
        public string Author { get; set; }
        [Display(Name = "Summary", Description = "Summary")]
        public string Summary { get; set; }
        [Display(Name = "Publisher", Description = "Publisher")]
        [MaxLength(128)]
        public string Publisher { get; set; }
        [Display(Name = "Pubplace", Description = "Pubplace")]
        [MaxLength(128)]
        public string Pubplace { get; set; }
        [Display(Name = "Page", Description = "Page")]
        public int? Page { get; set; }
        [Display(Name = "Price", Description = "Price")]
        public decimal? Price { get; set; }

        public decimal? PurchasingPrice { get; set; }
        [Display(Name = "Binding", Description = "Binding")]
        [MaxLength(128)]
        public string Binding { get; set; }
        [Display(Name = "ISBN", Description = "ISBN")]
        [MaxLength(30)]
        [Index(IsUnique =true)]
        public string ISBN { get; set; }
        [Display(Name = "ISBN10", Description = "ISBN10")]
        [MaxLength(30)]
         public string ISBN10 { get; set; }
        [Display(Name = "Keyword", Description = "Keyword")]
        [MaxLength(512)]
        public string Keyword { get; set; }

        [Display(Name = "Edition", Description = "Edition")]
        [MaxLength(128)]
        public string Edition { get; set; }
        [Display(Name = "Impression", Description = "Impression")]
        [MaxLength(128)]
        public string Impression { get; set; }
        [Display(Name = "Language", Description = "Language")]
        [MaxLength(128)]
        public string Language { get; set; }
        [Display(Name = "Format", Description = "Format")]
        [MaxLength(128)]
        public string Format { get; set; }
        [Display(Name = "Class", Description = "Class")]
        [MaxLength(128)]
        public string Class { get; set; }
        [Display(Name = "CIP", Description = "CIP")]
        [MaxLength(128)]
        public string CIP { get; set; }

        public int? Reads { get; set; }
        public int? Popular { get; set; }

        public Book()
        {
            this.Comments = new HashSet<Comment>();
            this.BookPictures = new HashSet<BookPicture>();
            this.CheckOuts = new HashSet<CheckOut>();
        }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<BookPicture> BookPictures { get; set; }
        public ICollection<CheckOut> CheckOuts { get; set; }

    }
}