using BlogsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Services
{
    public interface ICommentService
    {
        IList<CommentModel> GetCommentsByPost(int postId);
    }
}
