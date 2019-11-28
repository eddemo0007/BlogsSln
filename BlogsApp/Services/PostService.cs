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
        private ApplicationDbContext context;
        private ILogger<IPostService> logger;
        public PostService(ApplicationDbContext context, ILogger<IPostService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void ChangePostStatus(int postId, short status)
        {
            var post = context.Posts.FirstOrDefault(p => p.Id == postId);

            if(post != null)
            {
                post.Status = status;

                context.Posts.Update(post);

                context.SaveChanges();
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
                var publishedPosts = this.context.Posts.Where(p => p.Status == 2);

                var result = new List<PostViewModel>();
                foreach (var post in publishedPosts)
                {
                    var writer = context.Users.FirstOrDefault(u => u.Id == post.OwnerId);

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
                logger.LogError(ex.Message, ex);
                return new List<PostViewModel>();
            }
        }

        public void UpdatePost(PostViewModel updatedPost)
        {
            throw new NotImplementedException();
        }
    }
}
