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
    }
}
