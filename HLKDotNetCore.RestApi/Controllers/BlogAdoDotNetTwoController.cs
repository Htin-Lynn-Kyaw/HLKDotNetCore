using HLKDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using HLKDotNetCore.Shared;

namespace HLKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetTwoController : ControllerBase
    {
        private readonly Shared.AdoDotNetService _adoDotNetServiec = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            var list = _adoDotNetServiec.Query<BlogModel>(query);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from tbl_blog where blogID = @BlogID";

            AdoDotNetParameter[] param = new AdoDotNetParameter[1];
            param[0] = new AdoDotNetParameter("@BlogID", id);
            var item = _adoDotNetServiec.QueryFirstOrDefult<BlogModel>(query,param);

            if(item is null)
            {
                return NotFound("No Data Found.");
            }

            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel model)
        {
            string query1 = "select * from tbl_blog where blogID = @BlogID";
            AdoDotNetParameter[] param1 = new AdoDotNetParameter[1];
            param1[0] = new AdoDotNetParameter("@BlogID", id);
            var item = _adoDotNetServiec.QueryFirstOrDefult<BlogModel>(query1, param1);
            if (item is null) 
            {
                return NotFound("ID " + id + " not found.");
            }
            string query = @"UPDATE [dbo].[tbl_Blog]
                SET [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
                WHERE BlogID = @BlogID";


            AdoDotNetParameter[] param = new AdoDotNetParameter[4];
            param[0] = new AdoDotNetParameter("@BlogID", id);
            param[1] = new AdoDotNetParameter("@BlogTitle", model.BlogTitle!);
            param[2] = new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor!);
            param[3] = new AdoDotNetParameter("@BlogContent", model.BlogContent!);
            

            int result = _adoDotNetServiec.Execute(query, param);
            
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel model) 
        {
            string query = @"INSERT INTO [dbo].[tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle,
           @BlogAuthor,
           @BlogContent)";

            AdoDotNetParameter[] param = new AdoDotNetParameter[3];
            param[0] = new AdoDotNetParameter("@BlogTitle", model.BlogTitle!);
            param[1] = new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor!);
            param[2] = new AdoDotNetParameter("@BlogContent", model.BlogContent!);
            var item = _adoDotNetServiec.Execute(query, param);

            int result = _adoDotNetServiec.Execute(query, param);

            string message = result > 0 ? "Insert Successful" : "Insert Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[tbl_Blog]
                            WHERE BlogID = @BlogID";

            AdoDotNetParameter[] param1 = new AdoDotNetParameter[1];
            param1[0] = new AdoDotNetParameter("@BlogID", id);
            int result = _adoDotNetServiec.Execute(query, param1);

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog) 
        {
            string query1 = "select * from tbl_blog where blogID = @BlogID";
            AdoDotNetParameter[] param1 = new AdoDotNetParameter[1];
            param1[0] = new AdoDotNetParameter("@BlogID", id);
            var item = _adoDotNetServiec.QueryFirstOrDefult<BlogModel>(query1, param1);
            if (item is null)
            {
                return NotFound("ID " + id + " not found.");
            }

            AdoDotNetParameter[] param = new AdoDotNetParameter[4];

            string con = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                con += " [BlogTitle] = @BlogTitle, ";
                param[0] = new AdoDotNetParameter("@BlogTitle", blog.BlogTitle!);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                con += " [BlogAuthor] = @BlogAuthor, ";
                param[1] = new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor!);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                con += " [BlogContent] = @BlogContent, ";
                param[2] = new AdoDotNetParameter("@BlogContent", blog.BlogContent!);
            }
            if (string.IsNullOrEmpty(con))
            {
                return NotFound("No Data Found.");
            } 
            param[3] = new AdoDotNetParameter("@BlogID", id);

            con = con.Substring(0, con.Length - 2);
            string query = $@"UPDATE [dbo].[tbl_Blog]
                SET {con}
                WHERE BlogID = @BlogID";

            int result = _adoDotNetServiec.Execute(query, param);
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
    }
}
