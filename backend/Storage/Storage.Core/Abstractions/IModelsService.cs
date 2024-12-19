using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IModelsService
    {
        Task<Guid> CreateModel(Model model);
        Task<Guid> DeleteModel(Guid id);
        Task<List<Model>> GetAllModels();
        Task<Guid> UpdateModel(Guid id, string name, Guid categoryId);
    }
}
