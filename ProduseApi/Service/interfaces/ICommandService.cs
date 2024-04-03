using ProduseApi.Dto;
using ProduseApi.Models;

namespace ProduseApi.Service.interfaces
{
    public interface ICommandService
    {
        Task<Produs> Create(CreateRequest request);

        Task<Produs> Update(int id, UpdateRequest request);

        Task<Produs> Delete(int id);
    }
}
