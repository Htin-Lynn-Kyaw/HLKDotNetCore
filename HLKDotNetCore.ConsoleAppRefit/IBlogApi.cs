using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLKDotNetCore.ConsoleAppRefit
{
    public interface IBlogApi
    {
        [Get("/api/blog")]
        Task<List<BlogModel>> GetBlogs();

        [Get("/api/blog/{id}")]
        Task<BlogModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<string> CreateBlog(BlogModel blog);

        [Put("/api/blog/{id}")]
        Task<string> UpdateBlog(int id, BlogModel blog);

        [Delete("/api/blog/{id}")]
        Task<string> DeleteBlog(int id);

        [Patch("/api/blog/{id}")]
        Task<string> PatchBlog(int id, BlogModel blog);
    }
    public record BlogModel(
            int BlogID,
            string BlogTitle,
            string BlogAuthor,
            string BlogContent
        );
}
