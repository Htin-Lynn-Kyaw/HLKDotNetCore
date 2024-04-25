using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HLKDotNetCore.ConsoleApp
{

    internal class DapperExample
    {
        private readonly int _maxColumnWidth = 20;
        public void Run(
            //string operation, int blogID, string blogTitle, string blogAuthor, string blogContent
            )
        {
            Read();
            //switch (operation)
            //{
            //    case "Read": 
            //        Read(); break;
            //    case "Create": 
            //        Create(blogTitle, blogAuthor, blogContent); break;
            //    case "Update": 
            //        Update(blogID, blogTitle, blogAuthor, blogContent); break;
            //    case "Delete": 
            //        Delete(blogID); break;
            //    case "Edit": 
            //        Edit(blogID); break;
            //    default: return;
            //}

        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from tbl_blog").ToList();

            Console.WriteLine("| {0,-" + _maxColumnWidth + "} | {1,-" + _maxColumnWidth + "} | {2,-" + _maxColumnWidth + "} | {3,-" + _maxColumnWidth + "} |", "ID", "Title", "Author", "Content");
            Console.WriteLine(new string('-', (_maxColumnWidth + 4) * 4));
            foreach (BlogDto item in lst)
            {
                Console.WriteLine("| {0,-" + _maxColumnWidth + "} | {1,-" + _maxColumnWidth + "} | {2,-" + _maxColumnWidth + "} | {3,-" + _maxColumnWidth + "} |",
                        item.BlogID.ToString().Substring(0, Math.Min(_maxColumnWidth, item.BlogID.ToString().Length)),
                        item.BlogTitle.ToString().Substring(0, Math.Min(_maxColumnWidth, item.BlogTitle.ToString().Length)),
                        item.BlogAuthor.ToString().Substring(0, Math.Min(_maxColumnWidth, item.BlogAuthor.ToString().Length)),
                        item.BlogContent.ToString().Substring(0, Math.Min(_maxColumnWidth, item.BlogContent.ToString().Length)));
            }

        }
        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            BlogDto? item = db.Query<BlogDto>("select * from tbl_blog where blogID = @BlogID", new BlogDto() { BlogID = id }).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("Data not found.");
                return;
            }

            Console.WriteLine("| {0,-" + _maxColumnWidth + "} | {1,-" + _maxColumnWidth + "} | {2,-" + _maxColumnWidth + "} | {3,-" + _maxColumnWidth + "} |", "ID", "Title", "Author", "Content");
            Console.WriteLine(new string('-', (_maxColumnWidth + 4) * 4));

            Console.WriteLine("| {0,-" + _maxColumnWidth + "} | {1,-" + _maxColumnWidth + "} | {2,-" + _maxColumnWidth + "} | {3,-" + _maxColumnWidth + "} |",
                        item.BlogID.ToString().Substring(0, Math.Min(_maxColumnWidth, item.BlogID.ToString().Length)),
                        item.BlogTitle.ToString().Substring(0, Math.Min(_maxColumnWidth, item.BlogTitle.ToString().Length)),
                        item.BlogAuthor.ToString().Substring(0, Math.Min(_maxColumnWidth, item.BlogAuthor.ToString().Length)),
                        item.BlogContent.ToString().Substring(0, Math.Min(_maxColumnWidth, item.BlogContent.ToString().Length)));
        }

        private void Create(string title, string author, string content)
        {
            BlogDto item = new BlogDto()
            {
                BlogAuthor = author,
                BlogContent = content,
                BlogTitle = title,
            };

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
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            BlogDto item = new BlogDto()
            {
                BlogID = id,
                BlogAuthor = author,
                BlogContent = content,
                BlogTitle = title,
            };

            string query = @"UPDATE [dbo].[tbl_Blog]
                SET [BlogTitle] = @BlogTitle,
                [BlogAuthor] = @BlogAuthor,
                [BlogContent] = @BlogContent
                WHERE BlogID = @BlogID";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            BlogDto item = new BlogDto()
            {
                BlogID = id,

            };

            string query = @"Delete from [dbo].[tbl_Blog]
                WHERE BlogID = @BlogID";

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);

        }

    }
}
