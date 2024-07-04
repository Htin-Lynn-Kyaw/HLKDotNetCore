using HLKDotNetCore.MvcApp.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HLKDotNetCore.MvcApp.Models;

namespace HLKDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController()
        {
            _context = new AppDbContext();
        }

        public async Task<IActionResult> IndexAsync()
        {
            var lst = await _context.Blogs.ToListAsync();
            return View(lst);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            BlogModel model = (await _context.Blogs.FirstOrDefaultAsync(x => x.BlogID == id))!;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _context.Blogs.FirstOrDefaultAsync(x => x.BlogID == id);
            var response = new ResponseModel();

            if (model is null)
            {
                response.IsSucceed = false;
                response.Message = "ID " + id + "not found!";
                return Json(response);
            }

            _context.Blogs.Remove(model);
            int result = _context.SaveChanges();

            response.Message = result > 0 ? "Delete successful." : "Delete failed.";
            response.IsSucceed = result > 0;
            return Json(response);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Saving successful." : "Saving failed.";
            return Json(
                    new ResponseModel()
                    {
                        IsSucceed = result > 0,
                        Message = message
                    }
                );
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int id,BlogModel blog)
        {
            //_context.Blogs.Add(blog);
            var model = await _context.Blogs.FirstOrDefaultAsync(x => x.BlogID == id);
            var response = new ResponseModel();

            if(model is null)
            {
                response.IsSucceed = false;
                response.Message = "ID " + id + "not found!";
                return Json(response);
            }

            model.BlogTitle = blog.BlogTitle;
            model.BlogAuthor = blog.BlogAuthor;
            model.BlogContent = blog.BlogContent;
            int result = _context.SaveChanges();

            response.Message = result > 0 ? "Update successful." : "Update failed.";
            response.IsSucceed = result > 0;
            return Json(response);
        }
    }
}
