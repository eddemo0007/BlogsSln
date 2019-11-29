using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Models
{
    public class ManagePostViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public IList<PostViewModel> CreatedPosts { get; set; }
        public IList<PostViewModel> PendingPosts { get; set; }
    }
}
