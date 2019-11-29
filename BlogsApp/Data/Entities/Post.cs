using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string OwnerId { get; set; }
        [Required]
        [MaxLength(512)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        [Required]
        public short Status { get; set; } = 1;

        public List<Comment> Comments { get; set; }

    }
}
