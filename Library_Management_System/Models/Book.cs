using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Book
    {


        [Key]
        public int BookId { get; set; }
        [Required]
        [DisplayName("Book Title")]
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Edition { get; set; }
        [DisplayName("Publisher")]
        public int? PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        [ValidateNever]
        public virtual BookPublisher BookPublishers { get; set; }
    }

  
}
