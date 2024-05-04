using HLKDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;

namespace HLKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            connection.Close();

            //List<BlogModel> lst = new List<BlogModel>();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    BlogModel model = new BlogModel();
            //    model.BlogID = (int)dr["BlogId"];
            //    model.BlogTitle = (string)dr["BlogTitle"];
            //    model.BlogAuthor = (string)dr["BlogAuthor"];
            //    model.BlogContent = (string)dr["BlogContent"];
            //    lst.Add(model);
            //}

            List<BlogModel> list = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogID = (int)dr["BlogId"],
                BlogTitle = (string)dr["BlogTitle"],
                BlogAuthor = (string)dr["BlogAuthor"],
                BlogContent = (string)dr["BlogContent"]
            }).ToList();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from tbl_blog where blogID = @BlogID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            connection.Close();

            DataRow dr = dt.Rows[0];

            BlogModel model = new BlogModel
            {
                BlogID = (int)dr["BlogId"],
                BlogTitle = (string)dr["BlogTitle"],
                BlogAuthor = (string)dr["BlogAuthor"],
                BlogContent = (string)dr["BlogContent"]
            };

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel model)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query1 = "select * from tbl_blog where blogID = @BlogID";
            SqlCommand cmd1 = new SqlCommand(query1, connection);
            cmd1.Parameters.AddWithValue("@BlogID", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count is 0) 
            {
                return NotFound("ID " + id + " not found.");
            }
            connection.Open();
            string query = @"UPDATE [dbo].[tbl_Blog]
                SET [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
                WHERE BlogID = @BlogID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
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

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Insert Successful" : "Insert Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[tbl_Blog]
                            WHERE BlogID = @BlogID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog) 
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query1 = "select * from tbl_blog where blogID = @BlogID";
            SqlCommand cmd1 = new SqlCommand(query1, connection);
            cmd1.Parameters.AddWithValue("@BlogID", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count is 0)
            {
                return NotFound("ID " + id + " not found.");
            }
            connection.Open();


            string con = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
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
            if (string.IsNullOrEmpty(con))
            {
                return NotFound("No Data Found.");
            }
            con = con.Substring(0, con.Length - 2);
            string query = $@"UPDATE [dbo].[tbl_Blog]
                SET {con}
                WHERE BlogID = @BlogID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
    }
}
