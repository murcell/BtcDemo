using AutoFixture;
using BtcDemo.Client.Controllers;
using BtcDemo.Client.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace BtcDemo.ClientTest
{
    public class AccounControllerTest
    {
        private readonly AccountController _accountController;
        public Mock<IHttpClientFactory> _mockHttpClientFactory { get; set; }
        public Mock<IConfiguration> _mockConfiguration { get; set; }
    
        public AccounControllerTest()
        {
            //var authServiceMock = new Mock<IAuthenticationService>();
            //authServiceMock
            //    .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            //    .Returns(Task.FromResult((object)null));

            //var serviceProviderMock = new Mock<IServiceProvider>();
            //serviceProviderMock
            //    .Setup(_ => _.GetService(typeof(IAuthenticationService)))
            //    .Returns(authServiceMock.Object);

            _mockConfiguration = new Mock<IConfiguration>();
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _accountController = new AccountController(_mockHttpClientFactory.Object, _mockConfiguration.Object);
            //{
            //    ControllerContext = new ControllerContext
            //    {
            //        HttpContext = new DefaultHttpContext
            //        {
            //            // How mock RequestServices?
            //            RequestServices = serviceProviderMock.Object
            //        }
            //    }
            //};
            
        }

        [Fact]
        public void Login_ActionExecutes_ReturnView()
        {
            var result = _accountController.Login();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Login_InValidaModelState_ReturnView()
        {
            _accountController.ModelState.AddModelError("Name","Bir hata oluştu");
           
            var model = new UserLoginModel() { };
            
            var result = await _accountController.Login(model);
            
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsType<UserLoginModel>(viewResult.Model);

        }

        [Fact]
        public async void LoginPost_PostApIHttpClientFactory_ReturnResponseNotNull()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"data\":{\"accessToken\":\"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjI3MWM3Mzc3LTg2MWYtNGQwNS1hMDM4LTFiOWU1NzUwMjZhMyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtdXJzZWxAbXVyc2VsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlZpZXdlciIsImVtYWlsIjoibXVyc2VsQG11cnNlbC5jb20iLCJqdGkiOiI4MmFkZDM1Yy05NDAzLTQ1NzgtYTM3Ni0zMGE3MDA2YmMxNmUiLCJhdWQiOiJ3d3cuYXV0aHNlcnZlci5jb20iLCJuYmYiOjE2OTUxNTI0MTcsImV4cCI6MTY5NTE1MzMxNywiaXNzIjoid3d3LmF1dGhzZXJ2ZXIuY29tIn0.JRMDJIj2HaEQnRs-rnsE6znh0FA6bXxdDJX2rDmxUuU\",\"accessTokenExpiration\":\"2023-09-19T22:55:17.0248323+03:00\",\"refreshToken\":\"IHi1pbCYe7z1ybvZ7oF2aF+fG9rN4CA3cR8lOR175aE=\",\"refreshTokenExpiration\":\"2023-09-20T08:40:17.0250117+03:00\"},\"resultStatus\":0,\"message\":\"Token oluşturuldu\",\"exception\":null,\"validationErrors\":null}")
                });
          
            var _mockConfSection = new Mock<IConfigurationSection>();
            _mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "BaseUrl")]).Returns("http://localhost:5151");

            _mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "Services"))).Returns(_mockConfSection.Object);

            _mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "Services:BaseUrl")]).Returns(() => "http://localhost:5151");

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5151")
            };
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
          
            var model = new UserLoginModel() { Email= "mursel@mursel.com", Password=".7mp?*reP!" };


            

            await _accountController.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            var result = await _accountController.Login(model);



            //var content = new StringContent(JsonSerializer.Serialize(model), encoding: System.Text.Encoding.UTF8, "application/json");
            //var response = await client.PostAsync("api/auth/login", content);

            //var jsonData = await response.Content.ReadAsStringAsync();
            //var tokenModel = JsonSerializer.Deserialize<JwtResponse>(jsonData, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(tokenModel.Data.AccessToken);
            //var claims = token.Claims.ToList();
            //if (tokenModel.Data.AccessToken != null)
            //    claims.Add(new Claim("accessToken", tokenModel.Data.AccessToken));

            //var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            //var authProps = new AuthenticationProperties()
            //{
            //    ExpiresUtc = tokenModel.Data.AccessTokenExpiration,
            //    IsPersistent = true
            //};

            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //Assert.True(response.IsSuccessStatusCode);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Chart", redirect.ActionName);

        }

        [Fact]
        public async void LoginPost_PostApIHttpClientFactory_ReturnTokenModelFromResponseNotNull()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(new UserLoginModel() { }), encoding: System.Text.Encoding.UTF8, "application/json")
                });

            var _mockConfSection = new Mock<IConfigurationSection>();
            _mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "BaseUrl")]).Returns("http://localhost:5151");

            _mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "Services"))).Returns(_mockConfSection.Object);

            _mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "Services:BaseUrl")]).Returns(() => "http://localhost:5151");

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5151")
            };
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var model = new UserLoginModel() { };

            var content = new StringContent(JsonSerializer.Serialize(model), encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/auth/login", content);

            var jsonData = await response.Content.ReadAsStringAsync();
            var tokenModel = JsonSerializer.Deserialize<JwtResponse>(jsonData, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(tokenModel);
           
            //var resultView = await _accountController.Login(model);
            // Assert.NotNull(response);

            //Assert.Equal("Chart", redirect.ActionName);

            //var redirect = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void LoginPost_ValidModelState_ReturnRedirectToIndexAction()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(new UserLoginModel() { }), encoding: System.Text.Encoding.UTF8, "application/json")
                });

            var _mockConfSection = new Mock<IConfigurationSection>();
            _mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "BaseUrl")]).Returns("http://localhost:5151");

            _mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "Services"))).Returns(_mockConfSection.Object);

            _mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "Services:BaseUrl")]).Returns(() => "http://localhost:5151");

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost:5151")
            };
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var model = new UserLoginModel() { };

            var content = new StringContent(JsonSerializer.Serialize(model), encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/auth/login", content);

            var jsonData = await response.Content.ReadAsStringAsync();
            var tokenModel = JsonSerializer.Deserialize<JwtResponse>(jsonData, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //var result = await _accountController.Login(model);

            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //Assert.True(response.IsSuccessStatusCode);
            //Assert.NotNull(tokenModel);

            // Assert.NotNull(response);

            //Assert.Equal("Chart", redirect.ActionName);

            //var redirect = Assert.IsType<RedirectToActionResult>(result);
        }
        

    }




}
