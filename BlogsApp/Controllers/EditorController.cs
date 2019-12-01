using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EditorController : ControllerBase
    {
        private readonly IPostService _postService;

        public EditorController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("Pending")]
        public IActionResult Pending()
        {
            var pending = _postService.GetPostByStatus(PostStatus.Pending);

            if (pending.Count == 0)
            {
                return NotFound();
            }

            return Ok(pending);
        }

        [HttpPut]
        [Route("Publish/{id}")]
        public IActionResult Publish(int id)
        {
            _postService.ChangePostStatus(id, PostStatus.Published);
            return Ok();
        }

        [HttpPut]
        [Route("Reject/{id}")]
        public IActionResult Reject(int id)
        {
            _postService.ChangePostStatus(id, PostStatus.Rejected);
            return Ok();
        }
    }
}
