using Moq;
using ProduseApi.Dto;
using ProduseApi.Exceptions;
using ProduseApi.Service.interfaces;
using ProduseApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProduseApi.Repository.Interfaces;
using ProduseApi.Models;
using ProduseApi.Constants;
using Test.Helpers;

namespace Test.UnitTests
{
    public class TestCommandService
    {

        Mock<IProdusRepository> _mock;
        ICommandService _service;

        public TestCommandService()
        {
            _mock = new Mock<IProdusRepository>();
            _service = new CommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidPrice()
        {

            var create = new CreateRequest
            {
                Expirare = DateTime.Parse("04-10-2024"),
                Name = "test",
                Pret = -1
            };

            _mock.Setup(repo => repo.Create(create)).ReturnsAsync((Produs)null);

            var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _service.Create(create));

            Assert.Equal(Constants.InvalidPrice, exception.Message);


        }

        [Fact]
        public async Task Create_ReturnProdus()
        {
            var create = new CreateRequest
            {
                Expirare = DateTime.Parse("04-10-2024"),
                Name = "test",
                Pret = 10
            };

            var produs = TestProdusFactory.CreateProdus(5);
            produs.Expirare = create.Expirare;
            produs.Name = create.Name;
            produs.Pret = create.Pret;
            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(produs);

            var result = await _service.Create(create);

            Assert.NotNull(result);

            Assert.Equal(result, produs);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateRequest
            {
                Pret = 200
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Produs)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.Update(1, update));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);

        }

        [Fact]
        public async Task Update_InvalidPrice()
        {
            var update = new UpdateRequest
            {
                Pret = 0
            };

            var produs = TestProdusFactory.CreateProdus(5);
            produs.Pret = update.Pret.Value;


            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(produs);

            var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _service.Update(5, update));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task Update_ValidPrice()
        {
            var update = new UpdateRequest
            {
                Pret = 20
            };

            var produs = TestProdusFactory.CreateProdus(5);
            produs.Pret = update.Pret.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(produs);
            _mock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(produs);

            var result = await _service.Update(5, update);

            Assert.NotNull(result);
            Assert.Equal(produs, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((Produs)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.Delete(5));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);
        }

        [Fact]
        public async Task Delete_ValidPrice()
        {
            var produs = TestProdusFactory.CreateProdus(1);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produs);

            var result = await _service.Delete(1);

            Assert.NotNull(result);
            Assert.Equal(produs, result);
        }

    }
}
