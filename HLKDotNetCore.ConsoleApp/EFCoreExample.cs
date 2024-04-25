using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HLKDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
        private readonly AppDbContext db = new AppDbContext();
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

        private void Edit(int blogID)
        {
            var item=db.Blogs.FirstOrDefault(x => x.BlogID == blogID);
            if(item is null)
            {
                Console.WriteLine("No Data Found.");
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

        private void Delete(int blogID)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogID == blogID);
            if (item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }
            db.Blogs.Remove(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);
        }

        private void Update(int blogID, string blogTitle, string blogAuthor, string blogContent)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogID == blogID);
            if (item is null)
            {
                Console.WriteLine("No Data Found.");
                return;
            }

            item.BlogTitle = blogTitle;
            item.BlogAuthor = blogAuthor;
            item.BlogContent = blogContent;

            int result = db.SaveChanges();

            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }

        private void Create(string blogTitle, string blogAuthor, string blogContent)
        {
            BlogDto item = new BlogDto()
            {
                BlogAuthor = blogAuthor,
                BlogContent = blogContent,
                BlogTitle = blogTitle,
            };

            db.Blogs.Add(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Insert Successful" : "Insert Failed";
            Console.WriteLine(message);
        }

        private void Read()
        {
            List<BlogDto> lst = db.Blogs.ToList();

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
    }
}
