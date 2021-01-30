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
    //评论
    public partial class Comment:Entity
    {
        [Display(Name = "Content", Description = "Content")]
        [MaxLength(512)]
        public string Content { get; set; }
        [Display(Name = "User Name", Description = "User Name")]
        [MaxLength(20)]
        [DefaultValue("user")]
        public string UserName { get; set; }
        [Display(Name = "Display Name", Description = "Display Name")]
        [MaxLength(56)]
        public string DisplayName { get; set; }
        [Display(Name = "Date", Description = "Date")]
        [DefaultValue("now")]
        public DateTime PublishDate { get; set; }
        [Display(Name = "Evaluate", Description = "Evaluate")]
        public int Evaluate { get; set; }
        [Display(Name = "Comment", Description = "Comment")]
        public int ParentId { get; set; }
        [ForeignKey("ParentId")]
        [Display(Name = "Comment", Description = "Comment")]
        public Comment Parent { get; set; }


        [Display(Name = "Book", Description = "Book")]
        public int BookId { get; set; }
        [Display(Name = "Book", Description = "Book")]
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}