using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HLKDotNetCore.ConsoleAppHttpCiient
{
    internal class HttpClientExample
    {
        private readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7106")
        };
        private readonly string _endPoint = "api/blog";

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            await UpdateAsync(1, "Title 1", "author 2", "content 3");
            //await EditAsync(1);
            
            //await PatchAsync(1, title: "qwer" ,string.Empty,string.Empty);
            //await EditAsync(1);

        }
        public async Task ReadAsync()
        {
            var response = await _httpClient.GetAsync(_endPoint);
            string jasonStr = await response.Content.ReadAsStringAsync();
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
            var response = await _httpClient.GetAsync($"{_endPoint}/{id}");
            string jasonStr = await response.Content.ReadAsStringAsync();
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
            var response = await _httpClient.GetAsync($"{_endPoint}/{id}");
            string jasonStr = await response.Content.ReadAsStringAsync();
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
            string jsonContent = JsonConvert.SerializeObject(new BlogModel()
            {
                BlogAuthor = author,
                BlogContent = content,
                BlogTitle = title
            });
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PostAsync(_endPoint , httpContent);
            string message = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(message);
            }
        }
        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            string jsonContent = JsonConvert.SerializeObject(new BlogModel()
            {
                BlogAuthor = author,
                BlogContent = content,
                BlogTitle = title
            });
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PutAsync($"{_endPoint}/{id}", httpContent);
            string message = await response.Content.ReadAsStringAsync();
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
            string jsonContent = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PatchAsync($"{_endPoint}/{id}", httpContent);
            string message = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(message);
            }
        }
    }
}
