using Microsoft.AspNetCore.Mvc;
using Moq;
using ProduseApi.Constants;
using ProduseApi.Controllers;
using ProduseApi.Controllers.interfaces;
using ProduseApi.Dto;
using ProduseApi.Exceptions;
using ProduseApi.Models;
using ProduseApi.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helpers;

namespace Test.UnitTests
{
    public class TestController
    {

        Mock<ICommandService> _command;
        Mock<IQueryService> _query;
        ControllerAPI _controller;

        public TestController()
        {
            _command = new Mock<ICommandService>();
            _query = new Mock<IQueryService>();
            _controller = new ProductController(_query.Object, _command.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _query.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));
            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.ItemsDoNotExist, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidPrice()
        {
            var produss = TestProdusFactory.CreateProduse(5);

            _query.Setup(repo => repo.GetAll()).ReturnsAsync(produss);

            var result = await _controller.GetAll();

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            var produssAll = Assert.IsType<List<Produs>>(okresult.Value);

            Assert.Equal(5, produssAll.Count);
            Assert.Equal(200, okresult.StatusCode);

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

            _command.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ThrowsAsync(new InvalidPrice(Constants.InvalidPrice));

            var result = await _controller.CreateProdus(create);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.InvalidPrice, bad.Value);

        }

        [Fact]
        public async Task Create_ValidPrice()
        {
            var create = new CreateRequest
            {
                Expirare = DateTime.Parse("04-10-2024"),
                Name = "test",
                Pret = 100
            };
            var produs = TestProdusFactory.CreateProdus(5);
            produs.Pret = create.Pret;
            _command.Setup(repo => repo.Create(create)).ReturnsAsync(produs);

            var result = await _controller.CreateProdus(create);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(produs, okResult.Value);

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

            _command.Setup(repo => repo.Update(5, update)).ThrowsAsync(new InvalidPrice(Constants.InvalidPrice));

            var result = await _controller.UpdateProdus(5, update);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 400);
            Assert.Equal(bad.Value, Constants.InvalidPrice);


        }

        [Fact]
        public async Task Update_ValidPrice()
        {
            var update = new UpdateRequest
            {
                Pret = 200
            };
            var produs = TestProdusFactory.CreateProdus(5);
            produs.Pret = update.Pret.Value;

            _command.Setup(repo => repo.Update(5, update)).ReturnsAsync(produs);

            var result = await _controller.UpdateProdus(5, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, produs);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _command.Setup(repo => repo.Delete(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await _controller.DeleteProdus(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidPrice()
        {
            var produs = TestProdusFactory.CreateProdus(1);

            _command.Setup(repo => repo.Delete(1)).ReturnsAsync(produs);

            var result = await _controller.DeleteProdus(1);

            var okReult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okReult.StatusCode);
            Assert.Equal(produs, okReult.Value);

        }

    }
}
