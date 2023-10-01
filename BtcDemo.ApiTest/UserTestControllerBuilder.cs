using BtcDemo.API.Controllers;
using BtcDemo.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BtcDemo.ApiTest
{
    public class UserTestControllerBuilder
    {
        private ClaimsIdentity _identity;
        private ClaimsPrincipal _user;
        private ControllerContext _controllerContext;

        public UserTestControllerBuilder()
        {
            _identity = new ClaimsIdentity();
            _user = new ClaimsPrincipal(_identity);
            _controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = _user } };
        }

        public UserTestControllerBuilder WithClaims(IDictionary<string, string> claims)
        {
            _identity.AddClaims(claims.Select(c => new Claim(c.Key, c.Value)));
            return this;
        }

        public UserTestControllerBuilder WithIdentity(string userId, string userName)
        {
            _identity.AddClaims(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            });
            return this;
        }

        public UserTestControllerBuilder WithDefaultIdentityClaims()
        {
            _identity.AddClaims(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "testId"),
                new Claim(ClaimTypes.Name, "test@test.com")
            });
            return this;
        }

        public UsersController Build(IUserService userService)
        {
            return new UsersController(userService)
            {
                ControllerContext = _controllerContext
            };
        }
    }
}
