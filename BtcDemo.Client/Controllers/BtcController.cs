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

        public BtcController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       
        public async Task<IActionResult> List()
        {
            var token = User.Claims.FirstOrDefault(x=>x.Type== "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7063/api/");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("coins/getAllCoins");

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
                string actionUrl= string.Empty;
                var label = string.Empty;
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://localhost:7063/api/");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                switch (filterModel.Filter)
                {
                    case 0:
                        actionUrl = "coins/getAllCoins";
                        label = "All BTC Values";
                        break;
                    case 1:
                        actionUrl = "coins/getCoinsByLastOneHour";
                        label = "BTC Values of Last One Hour";
                        break;
                    case 2:
                        actionUrl = "coins/getCoinsByLastFiveHours";
                        label = "BTC Values of Last Five Hours";
                        break;
                    case 3:
                        actionUrl = "coins/getCoinsByLastSevenDays";
                        label = "BTC Values of Last Seven Hours";
                        break;
                    case 4:
                        actionUrl = "coins/getCoinsByLastOneMonth";
                        label = "BTC Values of Last One Month";
                        break;
                    case 5:
                        actionUrl = "coins/getCoinsByLastThreeHours";
                        label = "BTC Values of Last Three Hours";
                        break;
                    case 6:
                        actionUrl = $"coins/getCoinsByLastFourHours/{6}";
                        label = "BTC Values of Last Four Hours";
                        break;
                    default:
                        actionUrl = "coins/getAllCoins";
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
                        data.Add(new List<string>() { label + "Count : " + coinList.Data.Count.ToString() });
                        return Json(data);
                    }
                }
            }
            return Json(new List<object>());

        }

	}
}
