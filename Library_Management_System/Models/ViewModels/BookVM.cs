using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Models.ViewModels
{
    public class BookVM
    {
        public Book Books { get; set; }
        public BookPublisher BookPublishers { get; set; }

        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    
}
}
