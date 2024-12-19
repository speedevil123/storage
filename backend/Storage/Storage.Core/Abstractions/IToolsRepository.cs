using Storage.Core.Models;

namespace Storage.DataAccess.Repositories
{
    public interface IToolsRepository
    {
        Task<Guid> Create(Tool tool);
        Task<Guid> Delete(Guid id);
        Task<List<Tool>> Get();
        Task<Guid> Update(Guid id, int quantity, Guid modelId, Guid manufacturerId);
    }
}