using Dapper;
using HLKDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

namespace HLKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string querty = "select * from Tbl_blog";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(querty).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = FindByID(id);
            if(item is null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogModel item)
        {
            string query = @"INSERT INTO [dbo].[tbl_Blog]
               ([BlogTitle]
               ,[BlogAuthor]
               ,[BlogContent])
                VALUES
               (@BlogTitle,
               @BlogAuthor,
               @BlogContent)";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Insert Successful" : "Insert Failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            blog.BlogID = id;
            string query = @"UPDATE [dbo].[tbl_Blog]
                SET [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
                WHERE BlogID = @BlogID";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            blog.BlogID = id;

            string con = string.Empty;
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                con += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                con += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                con += " [BlogContent] = @BlogContent, ";
            }
            if(string.IsNullOrEmpty(con))
            {
                return NotFound("No Data Found.");
            }
            con = con.Substring(0, con.Length - 2);
            string query = $@"UPDATE [dbo].[tbl_Blog]
                SET {con}
                WHERE BlogID = @BlogID";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            string query = @"Delete from [dbo].[tbl_Blog]
                WHERE BlogID = @BlogID";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
        private BlogModel? FindByID(int id)
        {
            string query = "select * from tbl_blog where BlogID = @BlogID";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel() { BlogID = id }).FirstOrDefault();
            return item;
        }

    }
}
