using AutoMapper;
using BtcDemo.API.Controllers;
using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Services;
using BtcDemo.Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BtcDemo.ApiTest
{
    public class CoinsApiControllerTest
    {
        private readonly Mock<ICoinService> _mockCoinService;
        private readonly CoinsController _coinsController;
        private readonly IMapper _mapper;

        private List<CoinDto> _coins;

        public CoinsApiControllerTest()
        {
            _mockCoinService = new Mock<ICoinService>();
            _mapper = Mock.Of<IMapper>();
            _coinsController = new CoinsController(_mockCoinService.Object,_mapper);

            _coins = new List<CoinDto>() 
            {  
                new CoinDto(){ CreatedDate=DateTime.Now, CSupply=25, Id=4658786, MSupply=45, Name="Test 1", PercentChange1h=10, PercentChange24h=2, PercentChange7d= 50, PriceUsd=264569, Rank=2, Symbol="Btc", TSupply=34  },
                new CoinDto(){ CreatedDate=DateTime.Now, CSupply=25, Id=4658786, MSupply=45, Name="Test 2", PercentChange1h=10, PercentChange24h=2, PercentChange7d= 50, PriceUsd=264569, Rank=2, Symbol="Btc", TSupply=34  },
                new CoinDto(){ CreatedDate=DateTime.Now, CSupply=25, Id=4658786, MSupply=45, Name="Test 3", PercentChange1h=10, PercentChange24h=2, PercentChange7d= 50, PriceUsd=264569, Rank=2, Symbol="Btc", TSupply=34  },
                new CoinDto(){ CreatedDate=DateTime.Now, CSupply=25, Id=4658786, MSupply=45, Name="Test 4", PercentChange1h=10, PercentChange24h=2, PercentChange7d= 50, PriceUsd=264569, Rank=2, Symbol="Btc", TSupply=34  },
            };
        }

        [Fact]
        public async void GetCoins_ActionExecutes_ReturnOkResultWithCoins()
        {
            _mockCoinService.Setup(x => x.GetManyAsync(x=>x.IsDeleted==false)).ReturnsAsync(new DataResult<IList<CoinDto>>(ResultStatus.Success, _coins));

            var result = await _coinsController.GetCoins();
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<IEnumerable<CoinDto>>> (okResult.Value);

            Assert.Equal<int>(4, returnResult.Data.ToList().Count); 
        }

        [Theory]
        [InlineData(0)]
        public async void GetCoinsByFilter_ActionExecutesByInvalidFilter_ReturnOkResultWithNull(int dayFilter)
        {
            _mockCoinService.Setup(x => x.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddHours(-dayFilter))).ReturnsAsync(new DataResult<IList<CoinDto>>(ResultStatus.Success, null));

            var result = await _coinsController.GetCoinsByFilter(dayFilter);
            var okResult = Assert.IsType<OkObjectResult>(result);
           
            var returnResult = Assert.IsAssignableFrom<IDataResult<IEnumerable<CoinDto>>>(okResult.Value);
            Assert.Null(returnResult.Data);
          
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async void GetCoinsByFilter_ActionExecutesByFilter_ReturnOkResultWithCoins(int dayFilter)
        {
            _mockCoinService.Setup(x => x.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddHours(-dayFilter))).ReturnsAsync(new DataResult<IList<CoinDto>>(ResultStatus.Success, _coins));

            var result = await _coinsController.GetCoinsByFilter(dayFilter);
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<IEnumerable<CoinDto>>>(okResult.Value);
            
            Assert.NotNull(returnResult.Data);
            Assert.Equal<int>(4, returnResult.Data.ToList().Count);

        }

        [Fact]
        public async void GetCoinsByLastOneMonth_ActionExecutes_ReturnOkResultWithCoins()
        {
            _mockCoinService.Setup(x => x.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddMonths(-1))).ReturnsAsync(new DataResult<IList<CoinDto>>(ResultStatus.Success, _coins));

            var result = await _coinsController.GetCoinsByLastOneMonth();
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<IEnumerable<CoinDto>>>(okResult.Value);

            Assert.Equal<int>(4, returnResult.Data.ToList().Count);
        }

        [Fact]
        public async void GetCoinsByLastOneMonth_ActionExecutes_ReturnOkResultWithNull()
        {
            _mockCoinService.Setup(x => x.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddMonths(-1))).ReturnsAsync(new DataResult<IList<CoinDto>>(ResultStatus.Success, null));

            var result = await _coinsController.GetCoinsByLastOneMonth();
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<IEnumerable<CoinDto>>>(okResult.Value);
            Assert.Null(returnResult.Data);

        }

        [Fact]
        public async void GetCoinsByLastSevenDays_ActionExecutes_ReturnOkResultWithCoins()
        {
            _mockCoinService.Setup(x => x.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddDays(-7))).ReturnsAsync(new DataResult<IList<CoinDto>>(ResultStatus.Success, _coins));

            var result = await _coinsController.GetCoinsByLastSevenDays();
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<IEnumerable<CoinDto>>>(okResult.Value);

            Assert.Equal<int>(4, returnResult.Data.ToList().Count);
        }

        [Fact]
        public async void GetCoinsByLastSevenDays_ActionExecutes_ReturnOkResultWithNull()
        {
            _mockCoinService.Setup(x => x.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddDays(-7))).ReturnsAsync(new DataResult<IList<CoinDto>>(ResultStatus.Success, null));

            var result = await _coinsController.GetCoinsByLastSevenDays();
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<IDataResult<IEnumerable<CoinDto>>>(okResult.Value);
            Assert.Null(returnResult.Data);

        }
    }
}
