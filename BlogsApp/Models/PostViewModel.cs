using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime? PublishDate { get; set; }
        public WriterViewModel Writer { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
    }
}
