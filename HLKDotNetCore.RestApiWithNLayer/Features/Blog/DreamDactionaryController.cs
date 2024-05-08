using HLKDotNetCore.RestApiWithNLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HLKDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class DreamDactionaryController : ControllerBase
    {
        private readonly Dictionary<string, int> dict = new Dictionary<string, int>
            {
                { "က", 1 },{ "ခ", 2 },{ "ဂ", 3 },{ "င", 4 },{ "စ", 5 },{ "ဆ", 6 },{ "ဇ", 7 },
                { "ဈ", 8 },{ "ည", 9 },{ "တ", 10},{ "ထ", 11},{ "ဒ", 12},{ "ဓ", 13},{ "န", 14},
                { "ပ", 15},{ "ဖ", 16},{ "ဗ", 17},{ "ဘ", 18},{ "မ", 19},{ "ယ", 20},{ "ရ", 21},
                { "လ", 22},{ "ဝ", 23},{ "သ", 24},{ "ဟ", 25},{ "အ", 26}
            };
        private async Task<DreamDictionary> GetDreamDictionary()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var item = JsonConvert.DeserializeObject<DreamDictionary>(jsonStr);
            return item;
        }

        [HttpGet("getBlogHeaderData")]
        public async Task<IActionResult> getBlogHeaderData()
        {
            var item = await GetDreamDictionary();
            return Ok(item.BlogHeader);
        }
        [HttpGet("getBlogDetailData/{burmeseChar}")]
        public async Task<IActionResult> getBlogDetailData(string burmeseChar)
        {
            int ID = 0;
            if (dict.ContainsKey(burmeseChar))
            {
                ID = dict[burmeseChar];
            }
            var item = await GetDreamDictionary();
            return Ok(item.BlogDetail.Where(x => x.BlogId == ID));
        }
        [HttpGet("getDreamDictionary/{burmeseChar}/{detailID}")]
        public async Task<IActionResult> getDreamDictionary(string burmeseChar, int detailID)
        {
            int ID = 0;
            if (dict.ContainsKey(burmeseChar))
            {
                ID = dict[burmeseChar];
            }
            var item = await GetDreamDictionary();
            return Ok(item.BlogDetail.FirstOrDefault(x => x.BlogId == ID && x.BlogDetailId == detailID));
        }
    }
}
