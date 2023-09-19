using BtcDemo.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BtcDemo.Client.Controllers
{
    [Authorize]
    public class BtcController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BtcController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x=>x.Type== "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_configuration.GetSection("Services")["BaseUrl"]);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("api/coins/getAllCoins");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var coinList = JsonSerializer.Deserialize<CoinResponseModel> (jsonData, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                    if (coinList!=null && coinList.Data?.Count >0)
                    {
                        return View(coinList.Data);
                    }
                }
            }
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChartData(FilterModel filterModel)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                string actionUrl = $"api/coins/getCoinsByFilter/{filterModel.Filter}"; ;
                var label = string.Empty;
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_configuration.GetSection("Services")["BaseUrl"]);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                switch (filterModel.Filter)
                {
                    case 0:
                        label = "All BTC Values";
                        actionUrl = "api/coins/getAllCoins";
                        break;
                    case 1:
                        label = "BTC Values of Last One Hour";
                        break;
                    case 3:
                        label = "BTC Values of Last Three Hours";
                        break;
                    case 5:
                        label = "BTC Values of Last Five Hours";
                        break;
                    case 30:
                        actionUrl = "api/coins/getCoinsByLastOneMonth";
                        label = "BTC Values of Last One Month";
                        break;
                    case 7:
                        label = "BTC Values of Last Seven Days";
                        actionUrl = "api/coins/getCoinsByLastSevenDays";
                        break;
                    default:
                        actionUrl = "api/coins/getAllCoins";
                        break;
                }

               var response = client.GetAsync(actionUrl);

                List<object> data = new List<object>();
                if (response.Result.IsSuccessStatusCode)
                {
                    var jsonData = response.Result.Content.ReadAsStringAsync();
                    var coinList = JsonSerializer.Deserialize<CoinResponseModel>(jsonData.Result, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                    if (coinList != null && coinList.Data?.Count > 0)
                    {
                        data.Add(coinList.Data.Select(c=>c.CreatedDate.ToString()).ToList());
                        data.Add(coinList.Data.Select(c=>c.PriceUsd).ToList());
                        data.Add(new List<string>() { label + " Count : " + coinList.Data.Count.ToString() });
                        return Json(data);
                    }
                }
            }
            return Json(new List<object>());

        }

	}
}
