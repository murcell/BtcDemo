using BtcDemo.Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace BtcDemo.Client.Controllers;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public AccountController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
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

            client.BaseAddress = new Uri(_configuration.GetSection("Services")["BaseUrl"]);
            var content = new StringContent(JsonSerializer.Serialize(model), encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/auth/login", content);
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
                    // burası için mock yapamadım, çok zaman kaybettim.
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

    public IActionResult Register()
    {
        return View(new UserRegisterModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterModel model)
    {
        // Kullanıcı registeri api servsinden yaptım. 
        // Identity servislerini api içinde kullandım
        if (ModelState.IsValid)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetSection("Services")["BaseUrl"]);
            var content = new StringContent(JsonSerializer.Serialize(model), encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/users/createUser", content);
            if (response.IsSuccessStatusCode)
            {

                /*
                 string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string link = Url.Action("ConfirmEmail", "Home", new
                    {
                        userId = user.Id,
                        token = confirmationToken
                    }, protocol: HttpContext.Request.Scheme);

                    EmailConfirmation.SendEmail(link, user.Email);

                    return RedirectToAction("Login");
                 
                 */

                //var jsonData = await response.Content.ReadAsStringAsync();
                //Todo: EmailConfirmation yapısı kurululacak. bunun için İdentity user manager sınıflarını mvc tarafında kullanmak üzerine bir yapı kurulması gerek. Hepsi bir arada bulunsun diye api projesinde yaptım bu işlemleri.
                
                return RedirectToAction("Login", "Account");

                }
            }
        else
        {
            ModelState.AddModelError("", "Bir hata oluştu.");
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

}
