using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogsApp.Data;
using BlogsApp.Data.Entities;
using BlogsApp.Models;
using Microsoft.Extensions.Logging;

namespace BlogsApp.Services
{
    public class CommentService : ICommentService
    {
        private ApplicationDbContext _context;
        private ILogger<CommentService> _logger;

        public CommentService(ApplicationDbContext context, ILogger<CommentService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IList<CommentModel> GetCommentsByPost(int postId)
        {
            var comments = _context.Comments.Where(c => c.PostId == postId).OrderBy(c => c.PublishDate);

            var result = new List<CommentModel>();
            foreach(var c in comments)
            {
                result.Add(new CommentModel(c.Content, c.PublishDate));
            }

            return result;
        }
    }
}
