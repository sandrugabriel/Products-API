using Newtonsoft.Json;
using ProduseApi.Dto;
using ProduseApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.Helpers;
using Test.Infrastructure;

namespace Test.UnitTests
{
    public class TestProductIntegration : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TestProductIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllProducts_ProductsFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createProductRequest = TestProdusFactory.CreateProdus(1);
            var content = new StringContent(JsonConvert.SerializeObject(createProductRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Product/CreateProduct", content);

            var response = await _client.GetAsync("/api/v1/Product/All");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProductById_ProductFound_ReturnsOkStatusCode()
        {
            var createProductRequest = new CreateRequest
            { Expirare = DateTime.Now,Name="asd",Pret= 12 };
            var content = new StringContent(JsonConvert.SerializeObject(createProductRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/Product/CreateProduct", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Produs>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Name, createProductRequest.Name);
        }

        [Fact]
        public async Task GetProductById_ProductNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/Product/FindById?id=9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/Product/CreateProduct";
            var createProductRequest = new CreateRequest
            { Expirare = DateTime.Now, Name = "asd", Pret = 12 };
            var content = new StringContent(JsonConvert.SerializeObject(createProductRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Produs>(responseString);

            Assert.NotNull(result);
            Assert.Equal(createProductRequest.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/Product/CreateProduct";
            var createProduct = new CreateRequest
            { Expirare = DateTime.Now, Name = "asd", Pret = 12 };
            var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Produs>(responseString);

            request = $"/api/v1/Product/UpdateProduct?id={result.Id}";
            var updateProduct = new UpdateRequest { Name = "12test" };
            content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Produs>(responceStringUp);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Name, updateProduct.Name);
        }

        [Fact]
        public async Task Put_Update_InvalidProductPrice_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/Product/CreateProduct";
            var createProduct = new CreateRequest
            { Expirare = DateTime.Now, Name = "asd", Pret = 12 };
            var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Produs>(responseString);

            request = $"/api/v1/Product/UpdateProduct?id={result.Id}";
            var updateProduct = new UpdateRequest { Pret = 0 };
            content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Produs>(responseString);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result1.Name, updateProduct.Name);
        }

        [Fact]
        public async Task Put_Update_ProductDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/Product/UpdateProduct";
            var updateProduct = new UpdateRequest { Name = "asd" };
            var content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_ProductExists_ReturnsDeletedProduct()
        {
            var request = "/api/v1/Product/CreateProduct";
            var createProduct = new CreateRequest
            { Expirare = DateTime.Now, Name = "asd", Pret = 12 };
            var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Produs>(responseString)!;

            request = $"/api/v1/Product/DeleteProduct?id={result.Id}";

            response = await _client.DeleteAsync(request);
            var responceString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Produs>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Name, createProduct.Name);
        }

        [Fact]
        public async Task Delete_Delete_ProductDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/Product/DeleteProduct?id=7";

            var response = await _client.DeleteAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
