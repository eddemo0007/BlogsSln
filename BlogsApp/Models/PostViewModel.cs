using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Models
{
    public class PostViewModel
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime PublishDate { get; private set; }
        public WriterViewModel Writer { get; private set; }

        public PostViewModel(int id, string title, string content, DateTime publishDate, WriterViewModel writer)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.PublishDate = publishDate;
            this.Writer = writer;
        }
    }
}
