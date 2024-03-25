using ProduseApi.Models;
using System;

namespace ProduseApi.Repository.Interfaces
{
    public interface IProdusRepository
    {
        Task<IEnumerable<Produs>> GetAllAsync();
        Task<Produs> GetByNameAsync(string name);

        Task<Produs> GetByIdAsync(int id);
    }
}
