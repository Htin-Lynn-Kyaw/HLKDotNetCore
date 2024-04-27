using HLKDotNetCore.RestApi.DataBase;
using HLKDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HLKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BlogController()
        {
            _context = new AppDbContext();
        }
        
        [HttpGet]
        public IActionResult Read()
        {
            var list=_context.Blogs.ToList();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var list = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            if(list is null)
            {
                return NotFound("no data found.");
            }
            return Ok(list);
        }
        [HttpPost]
        public IActionResult Create(BlogModel model)
        {
            _context.Blogs.Add(model);
            int result=_context.SaveChanges();

            string message = result > 0 ? "Insert successful" : "Insert failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel model)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            if (item is null)
            {
                return NotFound("no data found.");
            }
            item.BlogTitle = model.BlogTitle;
            item.BlogAuthor = model.BlogAuthor;
            item.BlogContent = model.BlogContent;

            int result = _context.SaveChanges();

            string message = result > 0 ? "Update successful" : "Update failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel model)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            if (item is null)
            {
                return NotFound("no data found.");
            }
            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                item.BlogTitle = model.BlogTitle;
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                item.BlogAuthor = model.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                item.BlogContent = model.BlogContent;
            }
            int result = _context.SaveChanges();

            string message = result > 0 ? "Update successful" : "Update failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Blogs.Find(id);
            if (item is null)
            {
                return NotFound("no data found.");
            }
            _context.Blogs.Remove(item);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Deleting successful" : "Deleting failed";
            return Ok(message);
        }
    }
}
