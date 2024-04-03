using ProduseApi.Dto;
using ProduseApi.Exceptions;
using ProduseApi.Models;
using ProduseApi.Repository.Interfaces;
using ProduseApi.Service.interfaces;

namespace ProduseApi.Service
{
    public class CommandService : ICommandService
    {


        private IProdusRepository _repository;

        public CommandService(IProdusRepository repository)
        {
            _repository = repository;
        }

        public async Task<Produs> Create(CreateRequest request)
        {

            if (request.Pret <= 0)
            {
                throw new InvalidPrice(Constants.Constants.InvalidPrice);
            }

            var produs = await _repository.Create(request);

            return produs;
        }

        public async Task<Produs> Update(int id, UpdateRequest request)
        {

            var produs = await _repository.GetByIdAsync(id);
            if (produs == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }


            if (produs.Pret <= 0)
            {
                throw new InvalidPrice(Constants.Constants.InvalidPrice);
            }
            produs = await _repository.Update(id, request);
            return produs;
        }

        public async Task<Produs> Delete(int id)
        {

            var produs = await _repository.GetByIdAsync(id);
            if (produs == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }
            produs = await _repository.DeleteById(id);
            return produs;
        }
    }
}
