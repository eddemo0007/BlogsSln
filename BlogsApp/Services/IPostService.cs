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

        IList<PostViewModel> GetPostByUser(string userId);

        void ChangePostStatus(int postId, short status);

        void UpdatePost(PostViewModel updatedPost);
    }
}
