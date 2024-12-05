using Storage.Core.Models;
using System;

namespace Storage.Application.Services
{
    public interface IToolsService
    {
        Task<Guid> CreateTool(Tool tool);
        Task<Guid> DeleteTool(Guid id);
        Task<List<Tool>> GetAllTools();
        Task<Guid> UpdateTool(Guid id, string type, string model, string manufacturer, 
            int quantity, bool isTaken);
    }
}