using AutoMapper;
using BtcDemo.Client.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using BtcDemo.Core.DTOs;
using System.Net.Http.Json;

namespace BtcDemo.Client.Services;

public class AuthApiService: IAuthApiService
{
    private readonly HttpClient _httpClient;
    private IMapper _mapper;

    public AuthApiService(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<bool> Login(UserLoginModel model)
    {
        LoginDto loginDto = new LoginDto();
        loginDto.Email = model.Email;
        loginDto.Password = model.Password;

        var content = new StringContent(JsonSerializer.Serialize(model), encoding: System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsJsonAsync("auth/login",content);
        //var response = await _httpClient.PostAsJsonAsync<TokenDto>("auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var tokenModel = JsonSerializer.Deserialize<JwtTokenResponseModel>(jsonData);
            if (tokenModel != null)
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenModel.AccessToken);
                var claimsIdentity = new ClaimsIdentity(token.Claims, JwtBearerDefaults.AuthenticationScheme);
                var authProps = new AuthenticationProperties()
                {
                    ExpiresUtc = tokenModel.AccessTokenExpiration,
                    IsPersistent = true
                };

                // await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                //return RedirectToAction("Index");

                return true;
            }
        }   
        return false;
    }
    
}
