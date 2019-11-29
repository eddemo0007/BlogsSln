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

        void ChangePostStatus(int postId, short status);

        void UpdatePost(PostViewModel updatedPost);

        void CreatePost(string title, string content, string user);

        void DeletePost(int postId);
    }
}
