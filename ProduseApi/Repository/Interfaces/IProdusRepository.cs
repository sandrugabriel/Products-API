using ProduseApi.Models;
using System;
using ProduseApi.Dto;

namespace ProduseApi.Repository.Interfaces
{
    public interface IProdusRepository
    {
        Task<IEnumerable<Produs>> GetAllAsync();
        Task<Produs> GetByNameAsync(string name);

        Task<Produs> GetByIdAsync(int id);


        Task<Produs> Create(CreateRequest request);

        Task<Produs> Update(int id, UpdateRequest request);

        Task<Produs> DeleteById(int id);
    }
}
