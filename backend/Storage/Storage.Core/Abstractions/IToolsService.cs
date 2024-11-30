using Storage.Core.Models;

namespace Storage.Application.Services
{
    public interface IToolsService
    {
        Task<Guid> CreateTool(Tool tool);
        Task<Guid> DeleteTool(Guid id);
        Task<List<Tool>> GetAllTools();
        Task<Guid> UpdateTool(Guid id, string name, string model, string manufacturer, int stock);
    }
}