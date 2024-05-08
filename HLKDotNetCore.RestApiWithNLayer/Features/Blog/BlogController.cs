using HLKDotNetCore.RestApiWithNLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HLKDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;
        public BlogController()
        {
            _blBlog = new BL_Blog();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var list = _blBlog.GetBlogs();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var list = _blBlog.GetBlog(id);
            if (list is null)
            {
                return NotFound("no data found.");
            }
            return Ok(list);
        }
        [HttpPost]
        public IActionResult Create(BlogModel reqModel)
        {
            int result = _blBlog.CreateBlog(reqModel);

            string message = result > 0 ? "Insert successful" : "Insert failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("no data found.");
            }

            int result = _blBlog.UpdateBlog(id, blog);

            string message = result > 0 ? "Update successful" : "Update failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("no data found.");
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            int result = _blBlog.PatchBlog(id, blog);

            string message = result > 0 ? "Update successful" : "Update failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("no data found.");
            }
            int result = _blBlog.DeleteBlog(id);
            string message = result > 0 ? "Deleting successful" : "Deleting failed";
            return Ok(message);
        }
    }
}
