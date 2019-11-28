using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogsApp.Data;
using BlogsApp.Data.Entities;
using BlogsApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogsApp.Services
{
    public class PostService : IPostService
    {
        private ApplicationDbContext _context;
        private ILogger<IPostService> _logger;
        private ICommentService _commentService;

        public PostService(ApplicationDbContext context, ICommentService commentService, ILogger<PostService> logger)
        {
            _context = context;
            _logger = logger;
            _commentService = commentService;
        }

        public void ChangePostStatus(int postId, short status)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

            if(post != null)
            {
                post.Status = status;

                _context.Posts.Update(post);

                _context.SaveChanges();
            }
        }

        public IList<PostViewModel> GetPostByUser(string userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get published posts
        /// </summary>
        /// <returns>List of PostViewModel</returns>
        public IList<PostViewModel> GetPublishedPosts()
        {
            try
            {
                var publishedPosts = this._context.Posts.Where(p => p.Status == 2);

                var result = new List<PostViewModel>();
                foreach (var post in publishedPosts)
                {
                    var writer = _context.Users.FirstOrDefault(u => u.Id == post.OwnerId);

                    result.Add(new PostViewModel(
                        post.Id,
                        post.Title,
                        post.Content,
                        post.PublishDate,
                        new WriterViewModel(writer.Name, writer.LastName, writer.Email)));
                }
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new List<PostViewModel>();
            }
        }

        public void UpdatePost(PostViewModel updatedPost)
        {
            throw new NotImplementedException();
        }
    }
}
