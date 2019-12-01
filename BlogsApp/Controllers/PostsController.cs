using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogsApp.Models;
using BlogsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogsApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostsController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [Authorize(Roles = "Writer")]
        public IActionResult Create()
        {
            var createdPosts = _postService.GetPostByUserAndStatus(User.Identity.Name, PostStatus.Created);
            var pendingPosts = _postService.GetPostByUserAndStatus(User.Identity.Name, PostStatus.Pending);
            var rejectedPosts = _postService.GetPostByUserAndStatus(User.Identity.Name, PostStatus.Rejected);

            var model = new ManagePostViewModel
            {
                CreatedPosts = createdPosts,
                PendingPosts = pendingPosts,
                RejectedPosts = rejectedPosts
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Content")]ManagePostViewModel post)
        {
            if (ModelState.IsValid)
            {
                _postService.CreatePost(post.Title, post.Content, User.Identity.Name);
                return RedirectToAction("Create");
            }
            return View(post);
        }

        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var postToEdit = await _postService.GetPostByIdAsync(id);

            return View(postToEdit);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                _postService.UpdatePost(post);
                return RedirectToAction("Create");
            }
            return View(post);
        }

        [Authorize(Roles = "Writer")]
        public IActionResult Publish(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            _postService.ChangePostStatus(id, PostStatus.Pending);

            return RedirectToAction("Create");
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _postService.ChangePostStatus(id, PostStatus.Published);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Reject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _postService.ChangePostStatus(id, PostStatus.Rejected);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Remove(int id)
        {
            _postService.DeletePost(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Comment([Bind("Content,PostId")]ManageCommentsViewModel comment)
        {
            if (ModelState.IsValid)
            {
                _commentService.AddComent(comment.PostId, comment.Content);
                return RedirectToAction("Comment");
            }
            return View(comment);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Comment(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            var model = new ManageCommentsViewModel
            {
                Post = new PostViewModel
                {
                    Comments = post.Comments,
                    Content = post.Content,
                    Id = post.Id,
                    PublishDate = post.PublishDate,
                    Title = post.Title,
                    Writer = post.Writer
                }
            };

            return View(model);
        }

        [Authorize(Roles = "Writer,Editor")]
        public IActionResult Index()
        {
            IList<PostViewModel> createdPosts = null;
            IList<PostViewModel> pendingPosts;
            IList<PostViewModel> rejectedPosts;

            if (User.IsInRole("Writer"))
            {
                createdPosts = _postService.GetPostByUserAndStatus(User.Identity.Name, PostStatus.Created);
                pendingPosts = _postService.GetPostByUserAndStatus(User.Identity.Name, PostStatus.Pending);
                rejectedPosts = _postService.GetPostByUserAndStatus(User.Identity.Name, PostStatus.Rejected);
            } else
            {
                pendingPosts = _postService.GetPostByStatus(PostStatus.Pending);
                rejectedPosts = _postService.GetPostByStatus(PostStatus.Rejected);
            }

            

            var model = new PostIndexViewModel
            {
                CreatedPosts = createdPosts,
                PendingPosts = pendingPosts,
                RejectedPosts = rejectedPosts
            };

            return View(model);
        }
    }
}