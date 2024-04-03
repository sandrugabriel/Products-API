using Microsoft.AspNetCore.Mvc;
using ProduseApi.Controllers.interfaces;
using ProduseApi.Dto;
using ProduseApi.Exceptions;
using ProduseApi.Models;
using ProduseApi.Repository.Interfaces;
using ProduseApi.Service.interfaces;

namespace ProduseApi.Controllers
{
    public class ProductController : ControllerAPI
    {


        private IQueryService _queryService;
        private ICommandService _commandService;

        public ProductController(IQueryService queryService, ICommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<Produs>>> GetAll()
        {
            try
            {
                var produss = await _queryService.GetAll();

                return Ok(produss);

            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Produs>> GetByName([FromQuery] string name)
        {

            try
            {
                var produs = await _queryService.GetByNameAsync(name);
                return Ok(produs);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Produs>> GetById([FromQuery] int id)
        {

            try
            {
                var produs = await _queryService.GetById(id);
                return Ok(produs);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Produs>> CreateProdus(CreateRequest request)
        {
            try
            {
                var produs = await _commandService.Create(request);
                return Ok(produs);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Produs>> UpdateProdus([FromQuery] int id, UpdateRequest request)
        {
            try
            {
                var produs = await _commandService.Update(id, request);
                return Ok(produs);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Produs>> DeleteProdus([FromQuery] int id)
        {
            try
            {
                var produs = await _commandService.Delete(id);
                return Ok(produs);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
