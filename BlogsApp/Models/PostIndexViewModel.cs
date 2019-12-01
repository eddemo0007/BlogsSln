using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Models
{
    public class PostIndexViewModel
    {
        public IList<PostViewModel> CreatedPosts { get; set; }
        public IList<PostViewModel> PendingPosts { get; set; }
        public IList<PostViewModel> RejectedPosts { get; set; }
    }
}
