using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(512)]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public Post Post { get; set; }
    }
}
