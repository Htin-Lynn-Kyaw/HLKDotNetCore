using HLKDotNetCore.NLayer.DataAccess.Services;
using HLKDotNetCore.NLayer.DataAccess.Models;

namespace HLKDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;
        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }

        public List<BlogModel> GetBlogs()
        {
            return _daBlog.GetBlogs();
        }

        public BlogModel GetBlog(int id)
        {
            return _daBlog.GetBlog(id);
        }

        public int DeleteBlog(int id)
        {
            return _daBlog.DeleteBlog(id);
        }

        public int CreateBlog(BlogModel reqModel)
        {
            return _daBlog.CreateBlog(reqModel);
        }

        public int UpdateBlog(int id, BlogModel reqModel)
        {
            return _daBlog.UpdateBlog(id, reqModel);
        }

        public int PatchBlog(int id, BlogModel reqModel)
        {
            return _daBlog.PatchBlog(id, reqModel);
        }
    }
}
