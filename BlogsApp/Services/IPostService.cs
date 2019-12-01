using BlogsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Services
{
    public interface IPostService
    {
        IList<PostViewModel> GetPublishedPosts();

        IList<PostViewModel> GetPostByUserAndStatus(string userName, PostStatus status);
        IList<PostViewModel> GetPostByStatus(PostStatus status);

        Task<PostViewModel> GetPostByIdAsync(int? id);

        void ChangePostStatus(int? postId, PostStatus status);

        void UpdatePost(PostViewModel updatedPost);

        void CreatePost(string title, string content, string user);

        void DeletePost(int postId);
    }
}
