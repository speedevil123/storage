using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class ModelsService : IModelsService
    {
        private readonly IModelsRepository _modelsRepository;
        public ModelsService(IModelsRepository modelsRepository)
        {
            _modelsRepository = modelsRepository;
        }

        public async Task<Guid> CreateModel(Model model)
        {
            return await _modelsRepository.Create(model);
        }

        public async Task<Guid> DeleteModel(Guid id)
        {
            return await _modelsRepository.Delete(id);
        }

        public async Task<List<Model>> GetAllModels()
        {
            return await _modelsRepository.Get();
        }

        public async Task<Guid> UpdateModel(Guid id, string name, Guid categoryId)
        {
            return await _modelsRepository.Update(id, name, categoryId);
        }
    }
}
