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

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Create()
        {
            var createdPosts = _postService.GetPostByUserAndStatus(User.Identity.Name, PostStatus.Created);
            var pendingPosts = _postService.GetPostByUserAndStatus(User.Identity.Name, PostStatus.Pending);

            var model = new ManagePostViewModel
            {
                CreatedPosts = createdPosts,
                PendingPosts = pendingPosts
            };

            return View(model);
        }

        [HttpPost]
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
        public IActionResult Index()
        {
            return View();
        }
    }
}