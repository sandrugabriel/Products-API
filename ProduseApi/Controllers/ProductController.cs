using Microsoft.AspNetCore.Mvc;
using ProduseApi.Dto;
using ProduseApi.Models;
using ProduseApi.Repository.Interfaces;

namespace ProduseApi.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController:ControllerBase
    {

        private readonly ILogger<ProductController> _logger;

        private IProdusRepository _produsRepository;

        public ProductController(ILogger<ProductController> logger,IProdusRepository productRepository)
        {
            _logger = logger;
            _produsRepository = productRepository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Produs>>> GetAll()
        {
            var products = await _produsRepository.GetAllAsync();
            return Ok(products);
        }
        
        
        [HttpGet("/findById")]
        public async Task<ActionResult<Produs>> GetById([FromQuery] int id)
        {
            var produs = await _produsRepository.GetByIdAsync(id);
            return Ok(produs);
        }


        [HttpGet("/find/{name}")]
        public async Task<ActionResult<Produs>> GetByNameRoute([FromRoute] string name)
        {
            var produs = await _produsRepository.GetByNameAsync(name);
            return Ok(produs);
        }


        [HttpPost("/create")]
        public async Task<ActionResult<Produs>> Create([FromBody] CreateRequest request)
        {
            var movie = await _produsRepository.Create(request);
            return Ok(movie);

        }

        [HttpPut("/update")]
        public async Task<ActionResult<Produs>> Update([FromQuery] int id, [FromBody] UpdateRequest request)
        {
            var movie = await _produsRepository.Update(id, request);
            return Ok(movie);
        }

        [HttpDelete("/deleteById")]
        public async Task<ActionResult<Produs>> DeleteCarById([FromQuery] int id)
        {
            var movie = await _produsRepository.DeleteById(id);
            return Ok(movie);
        }


    }
}
