using BtcDemo.Client.Models;
using BtcDemo.Client.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;

namespace BtcDemo.Client.Controllers;

public class AccountController : Controller
{
    //private readonly AuthApiService _authApiService;
    //private readonly IHttpClientFactory _httpClientFactory;

    //public AccountController(AuthApiService authApiService, IHttpClientFactory httpClientFactory)
    //{
    //    _authApiService = authApiService;
    //    _httpClientFactory = httpClientFactory;
    //}

   
    private readonly IHttpClientFactory _httpClientFactory;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        
        _httpClientFactory = httpClientFactory;
    }


    public IActionResult Login()
    {
        return View(new UserLoginModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7063/api/");
            var content = new StringContent(JsonSerializer.Serialize(model), encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtResponse>(jsonData, new JsonSerializerOptions(){  PropertyNamingPolicy= JsonNamingPolicy.CamelCase });
                
                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Data.AccessToken);
                    var claims = token.Claims.ToList();
                    if (tokenModel.Data.AccessToken != null)
                        claims.Add(new Claim("accessToken", tokenModel.Data.AccessToken));

                    var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                    var authProps = new AuthenticationProperties()
                    {
                        ExpiresUtc = tokenModel.Data.AccessTokenExpiration,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                    return RedirectToAction("Chart","Btc");

                }
            }
            else
            {
                ModelState.AddModelError("", "Bir hata oluştu.");
            }
        }
       
        return View(model);
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index","Home");
    }



        [HttpPost]
    public async Task<IActionResult> Login1(UserLoginModel model)
    {
        //var result = await _authApiService.Login(model);

        return View(model);
    }

}
