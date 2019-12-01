using BlogsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsApp.Services
{
    public interface ICommentService
    {
        IList<CommentViewModel> GetCommentsByPost(int postId);

        void AddComent(int postId, string content);
    }
}
