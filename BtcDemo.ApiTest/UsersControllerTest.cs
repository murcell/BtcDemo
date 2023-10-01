using AutoMapper;
using BtcDemo.API.Controllers;
using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Services;
using BtcDemo.Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BtcDemo.ApiTest
{
    public class UsersControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UsersController _usersController;

        private List<AppUserDto> _users;

        public UsersControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
           
            _usersController = new UsersController(_mockUserService.Object);

            _users = new List<AppUserDto>()
            {
                new AppUserDto(){ Email = "test1@test.com", UserName = "test1@test.com"},
                new AppUserDto(){ Email = "test2@test.com", UserName = "test2@test.com"},
                new AppUserDto(){ Email = "test3@test.com", UserName = "test3@test.com"},
                new AppUserDto(){ Email = "test4@test.com", UserName = "test4@test.com"},
                new AppUserDto(){ Email = "test5@test.com", UserName = "test5@test.com"}
            };
        }

        [Fact]
        public async void CreateUser_ActionExecutes_ReturnOkResultWithUser()
        {
            CreateAppUserDto createAppUserDtoForSetup = null;
            CreateAppUserDto createAppUserDto = new CreateAppUserDto()
            {
                Email = "test@test.com",
                Password = "ps.?25?PS",
                UserName = "test@test.com"
            };
            AppUserDto appUserDto = new AppUserDto()
            {
                Email = "test@test.com",
                UserName = "test@test.com"
            };

            _mockUserService.Setup(x => x.CreateUserAsync(It.IsAny<CreateAppUserDto>())).Callback<CreateAppUserDto>(x=> createAppUserDtoForSetup = x).ReturnsAsync(new DataResult<AppUserDto>(ResultStatus.Success, appUserDto));

            var result = await _usersController.CreateUser(createAppUserDto);

            _mockUserService.Verify(repo => repo.CreateUserAsync(It.IsAny<CreateAppUserDto>()), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<AppUserDto>>(okResult.Value);

            Assert.NotNull(returnResult.Data);
            Assert.Equal(createAppUserDto.UserName, returnResult.Data.UserName);
        }

        [Fact]
        public async void GetUserByName_ActionExecutes_ReturnOkResultWithUser()
        {
            var userName = "test@test.com";
            AppUserDto appUserDto = new AppUserDto()
            {
                Email = "test@test.com",
                UserName = "test@test.com"
            };

            _mockUserService.Setup(x => x.GetUserByName(It.IsAny<string>())).ReturnsAsync(new DataResult<AppUserDto>(ResultStatus.Success, appUserDto));

            var customUsersController = new UserTestControllerBuilder().WithIdentity("test@test.com", "Test Doe").Build(_mockUserService.Object);

            var result = await customUsersController.GetUser();

            _mockUserService.Verify(repo => repo.GetUserByName(It.IsAny<string>()), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<AppUserDto>>(okResult.Value);

            Assert.NotNull(returnResult.Data);
            Assert.Equal(userName, returnResult.Data.UserName);

        }

        [Fact]
        public async void GetAllUsers_ActionExecutes_ReturnOkResultWithUser()
        {
            _mockUserService.Setup(x => x.GetAllUsers()).ReturnsAsync(new DataResult<List<AppUserDto>>(ResultStatus.Success, _users));

            var result = await _usersController.GetAllUser();

            _mockUserService.Verify(repo => repo.GetAllUsers(), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<List<AppUserDto>>>(okResult.Value);

            Assert.NotNull(returnResult.Data);
            Assert.Equal<int>(5, returnResult.Data.ToList().Count);
        }
    }
}
