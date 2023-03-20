using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class BookPublisher
    {

        [Key]
        public int PublisherId { get; set; }
        [Required]
        [DisplayName("Publisher Name")]
        public string PublName { get; set; }
        [Required]
        [DisplayName("Publisher Address")]
        public string PubAdd { get; set; }
        [Required]
        [DisplayName("Publisher Contact")]
        public string PubContact { get; set; }

    }
}
