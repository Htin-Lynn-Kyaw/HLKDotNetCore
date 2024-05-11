using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HLKDotNetCore.ConsoleAppRestClient
{
    internal class RestClientExample
    {
        private readonly RestClient _httpClient = new RestClient(new Uri("https://localhost:7106"));
        private readonly string _endPoint = "api/blog";

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await UpdateAsync(1, "Title 1", "author 2", "content 3");
            //await EditAsync(1);

            await PatchAsync(1, title: "qwer" ,string.Empty,string.Empty);
            //await EditAsync(1);

        }
        public async Task ReadAsync()
        {
            RestRequest restRequest = new RestRequest(_endPoint, Method.Get);
            var response = await _httpClient.GetAsync(restRequest);
            string jasonStr = response.Content!;
            if (response.IsSuccessStatusCode)
            {
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jasonStr)!;

                foreach (var blog in lst)
                {
                    Console.WriteLine($"BlogID => {blog.BlogID}");
                    Console.WriteLine($"BlogAuthor => {blog.BlogAuthor}");
                    Console.WriteLine($"BlogTitle => {blog.BlogTitle}");
                    Console.WriteLine($"BlogContent => {blog.BlogContent}");
                }

            }
        }
        public async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest(_endPoint, Method.Get);
            var response = await _httpClient.GetAsync(restRequest);
            string jasonStr = response.Content!;
            if (response.IsSuccessStatusCode)
            {
                BlogModel blog = JsonConvert.DeserializeObject<BlogModel>(jasonStr)!;
                Console.WriteLine($"BlogID => {blog.BlogID}");
                Console.WriteLine($"BlogAuthor => {blog.BlogAuthor}");
                Console.WriteLine($"BlogTitle => {blog.BlogTitle}");
                Console.WriteLine($"BlogContent => {blog.BlogContent}");
            }
            else
            {
                Console.WriteLine(jasonStr);
            }
        }

        public async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest(_endPoint, Method.Get);
            var response = await _httpClient.GetAsync(restRequest);
            string jasonStr = response.Content!;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(jasonStr);
            }
            else
            {
                Console.WriteLine(jasonStr);
            }
        }
        private async Task CreateAsync(string title, string author, string content)
        {
            BlogModel model =  new BlogModel()
            {
                BlogAuthor = author,
                BlogContent = content,
                BlogTitle = title
            };
            RestRequest restRequest = new RestRequest(_endPoint, Method.Post);
            restRequest.AddJsonBody(model);
            var response = await _httpClient.PostAsync(restRequest);
            string message = response.Content!;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(message);
            }
        }
        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogModel model = new BlogModel()
            {
                BlogAuthor = author,
                BlogContent = content,
                BlogTitle = title
            };
            RestRequest restRequest = new RestRequest($"{_endPoint}/{id}", Method.Put);
            restRequest.AddJsonBody(model);
            var response = await _httpClient.PutAsync(restRequest);
            string message = response.Content!;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(message);
            }
        }
        private async Task PatchAsync(int id, string title, string author, string content)
        {
            BlogModel model = new BlogModel();
            if (!string.IsNullOrEmpty(title))
            {
                model.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(author))
            {
                model.BlogAuthor = author;
            }
            if (!string.IsNullOrEmpty(content))
            {
                model.BlogContent = content;
            }
            RestRequest restRequest = new RestRequest($"{_endPoint}/{id}", Method.Patch);
            restRequest.AddJsonBody(model);
            var response = await _httpClient.PatchAsync(restRequest);
            string message = response.Content!;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(message);
            }
        }
    }
}
