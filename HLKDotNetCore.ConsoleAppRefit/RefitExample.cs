using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLKDotNetCore.ConsoleAppRefit
{
    public class RefitExample
    {
        private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7228");
        public async Task RunAsync()
        {
            //await CreateAsync(new BlogModel(0,"hhhh","hhhh","hhhh"));
            //await ReadAsync();
            await UpdateAsync(28, new BlogModel(0, "oooo", "oooo", "oooo"));
            await EditAsync(28);
            //await PatchAsync(27, new BlogModel(0, "", "wwww", ""));
            //await EditAsync(27);
            //await DeleteAsync(27);
            //await EditAsync(27);
        }
        private async Task ReadAsync()
        {
            var lst = await _service.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogID);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("----------------------------");
            }
        }
        private async Task EditAsync(int id)
        {
            try
            {
                var item = await _service.GetBlog(id);
                Console.WriteLine(item.BlogID);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogContent);

            }
            catch (ApiException ae)
            {
                Console.WriteLine(ae.Content);
            }
        }

        private async Task CreateAsync(BlogModel requestModel)
        {
            var message = await _service.CreateBlog(requestModel);
            Console.WriteLine(message);
        }

        private async Task UpdateAsync(int id, BlogModel requestModel)
        {
            var message = await _service.UpdateBlog(id, requestModel);
            Console.WriteLine(message);
        }

        private async Task DeleteAsync(int id)
        {
            var message = await _service.DeleteBlog(id);
            Console.WriteLine(message);
        }

        private async Task PatchAsync(int id, BlogModel requstModel)
        {
            var message = await _service.PatchBlog(id, requstModel);
            Console.WriteLine(message);
        }
    }
}
