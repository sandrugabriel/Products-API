using ProduseApi.Exceptions;
using ProduseApi.Models;
using ProduseApi.Repository.Interfaces;
using ProduseApi.Service.interfaces;

namespace ProduseApi.Service
{
    public class QueryService : IQueryService
    {
        private IProdusRepository _repository;

        public QueryService(IProdusRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Produs>> GetAll()
        {
            var produs = await _repository.GetAllAsync();

            if (produs.Count() == 0)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return (List<Produs>)produs;
        }

        public async Task<Produs> GetByNameAsync(string name)
        {
            var produs = await _repository.GetByNameAsync(name);

            if (produs == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return produs;
        }

        public async Task<Produs> GetById(int id)
        {
            var produs = await _repository.GetByIdAsync(id);

            if (produs == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return produs;
        }
    }
}
