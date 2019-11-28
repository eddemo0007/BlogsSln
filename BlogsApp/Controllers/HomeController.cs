using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogsApp.Models;
using Microsoft.AspNetCore.Authorization;
using BlogsApp.Services;

namespace BlogsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;

        public HomeController(IPostService postService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _postService = postService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var publishedPosts = _postService.GetPublishedPosts();
            return View(publishedPosts);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
