using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Models
{
    public class ManageCommentsViewModel
    {
        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int PostId { get; set; }

        public PostViewModel Post { get; set; }
    }
}
