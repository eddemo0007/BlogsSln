using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Models
{
    public class CommentModel
    {
        public string Content { get; private set; }
        public DateTime PublishDate { get; private set; }

        public CommentModel(string content, DateTime publishDate)
        {
            Content = content;
            PublishDate = publishDate;
        }
    }
}
