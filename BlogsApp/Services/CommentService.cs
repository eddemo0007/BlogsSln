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

        public void AddComent(int postId, string content)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

            post.Comments = _context.Comments.Where(c => c.Post.Id == postId).ToList();

            post.Comments.Add(new Comment
            {
                Content = content,
                PublishDate = DateTime.UtcNow
            });

            _context.SaveChanges();
        }

        public IList<CommentViewModel> GetCommentsByPost(int postId)
        {
            var comments = _context.Comments.Where(c => c.Post.Id == postId).OrderByDescending(c => c.PublishDate);

            var result = new List<CommentViewModel>();
            foreach(var c in comments)
            {
                result.Add(new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    PublishDate = c.PublishDate
                });
            }

            return result;
        }
    }
}
