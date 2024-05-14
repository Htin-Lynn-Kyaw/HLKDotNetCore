using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HLKDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarProverbsController : ControllerBase
    {
        private async Task<MyanmarProverbs> GetAsyncDataFromApi()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/sannlynnhtun-coding/Myanmar-Proverbs/main/MyanmarProverbs.json");
            if (!response.IsSuccessStatusCode) return null!;

            string jsonStr = await response.Content.ReadAsStringAsync();
            MyanmarProverbs mprov = JsonConvert.DeserializeObject<MyanmarProverbs>(jsonStr)!;
            return mprov;
        }

        [HttpGet]
        public async Task<IActionResult> GetChars()
        {
            var model = await GetAsyncDataFromApi();
            return Ok(model.Tbl_MMProverbsTitle);
        }

        [HttpGet("{burmeseChar}")]
        public async Task<IActionResult> GetByProverbsChar(string burmeseChar)
        {
            var model = await GetAsyncDataFromApi();
            var item = model.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == burmeseChar);
            if (item is null) return NotFound();
            int titleID = item.TitleId;
            var proverb = model.Tbl_MMProverbs.Where(x => x.TitleId == titleID).Select(s => new { s.TitleId, s.ProverbId, s.ProverbName });
            return Ok(proverb);
        }

        [HttpGet("{proverbID}/{titleID}")]
        public async Task<IActionResult> GetByProverbsID(int proverbID, int titleID)
        {
            var model = await GetAsyncDataFromApi();
            var prov = model.Tbl_MMProverbs.Where(x => x.ProverbId == proverbID && x.TitleId == titleID).Select(x => new { x.ProverbName, x.ProverbDesp });
            return Ok(prov);
        }
    }

    public class MyanmarProverbs
    {
        public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
        public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }
    }

    public class Tbl_Mmproverbstitle
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class Tbl_Mmproverbs
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }

}
