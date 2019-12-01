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

        /// <summary>
        /// Modify the status of the existing post
        /// </summary>
        /// <param name="postId">Post identifier</param>
        /// <param name="status">New status</param>
        public void ChangePostStatus(int? postId, PostStatus status)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

            if(post != null)
            {
                post.Status = (short)status;

                _context.Posts.Update(post);

                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Create a new post but does not request the approval to publish it
        /// </summary>
        /// <param name="title">Title of the post</param>
        /// <param name="content">Content of the post</param>
        /// <param name="user">User who creates the post</param>
        public void CreatePost(string title, string content, string user)
        {
            var writer = this.GetUser(user);

            if(user == null)
            {
                return;
            }

            var post = new Post
            {
                Title = title,
                Content = content,
                OwnerId = writer.Id,
                PublishDate = DateTime.UtcNow,
                Status = (short)PostStatus.Created
            };

            _context.Posts.Add(post);

            _context.SaveChanges();
        }

        /// <summary>
        /// Remove a post from the system
        /// </summary>
        /// <param name="postId"></param>
        public void DeletePost(int postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

            _context.Posts.Remove(post);

            _context.SaveChanges();
        }

        public async Task<PostViewModel> GetPostByIdAsync(int? id)
        {
            var postEntity = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == postEntity.OwnerId);

            return new PostViewModel
            {
                Id = postEntity.Id,
                Comments = _commentService.GetCommentsByPost(postEntity.Id),
                Content = postEntity.Content,
                Title = postEntity.Title,
                PublishDate = postEntity.PublishDate,
                Writer = new WriterViewModel
                {
                    Email = user.Email,
                    LastName = user.LastName,
                    Name = user.Name
                }
            };
        }

        public IList<PostViewModel> GetPostByStatus(PostStatus status)
        {
            var posts = _context.Posts.Where(p => p.Status == (short)status).OrderByDescending(p => p.PublishDate);

            var result = new List<PostViewModel>();
            foreach (var p in posts)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == p.OwnerId);

                result.Add(new PostViewModel
                {
                    Id = p.Id,
                    Comments = _commentService.GetCommentsByPost(p.Id),
                    Content = p.Content,
                    PublishDate = p.PublishDate,
                    Title = p.Title,
                    Writer = new WriterViewModel
                    {
                        Email = user.Email,
                        LastName = user.LastName,
                        Name = user.Name
                    }
                });
            }
            return result;
        }

        public IList<PostViewModel> GetPostByUserAndStatus(string userName, PostStatus status)
        {
            var user = this.GetUser(userName);

            var posts = _context.Posts.Where(p => p.OwnerId == user.Id && p.Status == (short)status).OrderByDescending(p => p.PublishDate);

            var result = new List<PostViewModel>();
            foreach(var p in posts)
            {
                result.Add(new PostViewModel
                {
                    Id = p.Id,
                    Comments = _commentService.GetCommentsByPost(p.Id),
                    Content = p.Content,
                    PublishDate = p.PublishDate,
                    Title = p.Title,
                    Writer = new WriterViewModel
                    {
                        Email = user.Email,
                        LastName = user.LastName,
                        Name = user.Name
                    }
                });
            }
            return result;
        }

        /// <summary>
        /// Get all published posts
        /// </summary>
        /// <returns>List of PostViewModel</returns>
        public IList<PostViewModel> GetPublishedPosts()
        {
            try
            {
                var publishedPosts = this._context.Posts.Where(p => p.Status == (short)PostStatus.Published).OrderByDescending(p => p.PublishDate);

                var result = new List<PostViewModel>();
                foreach (var post in publishedPosts)
                {
                    var writer = _context.Users.FirstOrDefault(u => u.Id == post.OwnerId);

                    result.Add(new PostViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Content = post.Content,
                        PublishDate = post.PublishDate,
                        Writer = new WriterViewModel
                        {
                            Email = writer.Email,
                            Name = writer.Name,
                            LastName = writer.LastName
                        },
                        Comments = _commentService.GetCommentsByPost(post.Id)
                    });
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
            var post = _context.Posts.FirstOrDefault(p => p.Id == updatedPost.Id);

            if(post == null)
            {
                return;
            }

            // Published or Pending posts cannot be updated
            if(post.Status == (short)PostStatus.Pending || post.Status == (short)PostStatus.Published)
            {
                return;
            }

            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;

            _context.Posts.Update(post);

            _context.SaveChanges();
        }

        private ApplicationUser GetUser(string user)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == user);
        }
    }
}
