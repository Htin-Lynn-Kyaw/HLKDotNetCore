using HLKDotNetCore.RestApiWithNLayer.DataBase;
using HLKDotNetCore.RestApiWithNLayer.Models;

namespace HLKDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class DA_Blog
    {
        private readonly AppDbContext _context;
        public DA_Blog()
        {
            _context = new AppDbContext();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _context.Blogs.ToList();
            return lst;
        }

        public BlogModel GetBlog(int id) 
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            return blog;
        }

        public int DeleteBlog(int id) 
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            if(item is null)
            {
                return 0;
            }

            _context.Blogs.Remove(item);
            return _context.SaveChanges();
        }

        public int CreateBlog(BlogModel reqModel)
        {
            _context.Blogs.Add(reqModel);
            return _context.SaveChanges();
        }

        public int UpdateBlog(int id, BlogModel reqModel) 
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            if (item is null)
            {
                return 0;
            }

            item.BlogContent = reqModel.BlogContent;
            item.BlogAuthor = reqModel.BlogAuthor;
            item.BlogTitle = reqModel.BlogTitle;

            return _context.SaveChanges();
        }

        public int PatchBlog(int id, BlogModel reqModel)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogID == id);
            if (item is null)
            {
                return 0;
            }

            if(!string.IsNullOrEmpty(reqModel.BlogContent))
            {
                item.BlogContent = reqModel.BlogContent;
            }
            if(!string.IsNullOrEmpty(reqModel.BlogAuthor))
            {
                item.BlogAuthor = reqModel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(reqModel.BlogTitle))
            {
                item.BlogTitle = reqModel.BlogTitle;
            }

            return _context.SaveChanges();
        }
    }
}
