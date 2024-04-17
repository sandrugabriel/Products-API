using Moq;
using ProduseApi.Constants;
using ProduseApi.Exceptions;
using ProduseApi.Models;
using ProduseApi.Repository.Interfaces;
using ProduseApi.Service;
using ProduseApi.Service.interfaces;
using Test.Helpers;

namespace Test.UnitTests
{
    public class TestQueryService
    {
            Mock<IProdusRepository> _mock;
            IQueryService _service;

            public TestQueryService()
            {
                _mock = new Mock<IProdusRepository>();
                _service = new QueryService(_mock.Object);
            }

            [Fact]
            public async Task GetAll_ItemsDoNotExist()
            {
                _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Produs>());

                var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAll());

                Assert.Equal(exception.Message, Constants.ItemsDoNotExist);

            }

            [Fact]
            public async Task GetAll_ReturnAllProdus()
            {
                var produss = TestProdusFactory.CreateProduse(5);

                _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(produss);

                var result = await _service.GetAll();

                Assert.NotNull(result);
                Assert.Contains(produss[1], result);

            }

            [Fact]
            public async Task GetById_ItemDoesNotExist()
            {
                _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Produs)null);

                var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

                Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
            }

            [Fact]
            public async Task GetById_ReturnProdus()
            {

                var produs = TestProdusFactory.CreateProdus(5);

                _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(produs);

                var result = await _service.GetById(5);

                Assert.NotNull(result);
                Assert.Equal(produs, result);

            }

            [Fact]
            public async Task GetByName_ItemDoesNotExist()
            {

                _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((Produs)null);

                var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByNameAsync(""));

                Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
            }

            [Fact]
            public async Task GetByName_ReturnProdus()
            {
                var produs = TestProdusFactory.CreateProdus(3);

                produs.Name = "test";
                _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(produs);

                var result = await _service.GetByNameAsync("test");

                Assert.NotNull(result);
                Assert.Equal(produs, result);
            }

        
    }
}