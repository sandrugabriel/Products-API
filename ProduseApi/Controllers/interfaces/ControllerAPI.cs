using Microsoft.AspNetCore.Mvc;
using ProduseApi.Dto;
using ProduseApi.Models;

namespace ProduseApi.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPI : ControllerBase
    {


        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Produs>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Produs>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Produs))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Produs>> GetById([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(Produs))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Produs>> GetByName([FromQuery] string name);

        [HttpPost("CreateProduct")]
        [ProducesResponseType(statusCode: 201, type: typeof(Produs))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Produs>> CreateProdus(CreateRequest request);

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(statusCode: 200, type: typeof(Produs))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Produs>> UpdateProdus([FromQuery] int id, UpdateRequest request);

        [HttpDelete("DeleteProduct")]
        [ProducesResponseType(statusCode: 200, type: typeof(Produs))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Produs>> DeleteProdus([FromQuery] int id);
    }
}
