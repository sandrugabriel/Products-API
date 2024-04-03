using ProduseApi.Models;

namespace ProduseApi.Service.interfaces
{
    public interface IQueryService
    {
        Task<List<Produs>> GetAll();

        Task<Produs> GetById(int id);

        Task<Produs> GetByNameAsync(string name);
    }
}
