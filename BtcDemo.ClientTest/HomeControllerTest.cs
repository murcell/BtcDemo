using BtcDemo.Client.Controllers;
using BtcDemo.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace BtcDemo.ClientTest
{
    public class HomeControllerTest
    {

        public HomeController _homeController { get; set; }
        public Mock _mock;
        public HomeControllerTest()
        {
            _mock = new Mock<HomeController>();
            _homeController = new HomeController();
        }

        [Fact]
        public void Idex_ActionExecutes_ReturnView() 
        {
            var result = _homeController.Index();
            // test yorum
            Assert.IsType<ViewResult>(result);
        }

    }
}