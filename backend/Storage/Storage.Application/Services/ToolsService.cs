﻿
using Storage.Core.Models;
using Storage.DataAccess.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace Storage.Application.Services
{
    public class ToolsService : IToolsService
    {
        private readonly IToolsRepository _toolsRepository;
        public ToolsService(IToolsRepository toolsRepository)
        {
            _toolsRepository = toolsRepository;
        }

        public async Task<Guid> CreateTool(Tool tool)
        {
            return await _toolsRepository.Create(tool);
        }

        public async Task<Guid> DeleteTool(Guid id)
        {
            return await _toolsRepository.Delete(id);
        }

        public async Task<List<Tool>> GetAllTools()
        {
            return await _toolsRepository.Get();
        }

        public async Task<Guid> UpdateTool(Guid id, int Quantity, Guid modelId, Guid manufacturerId)
        {
            return await _toolsRepository.Update(id, Quantity, modelId, manufacturerId);
        }


    }
}

