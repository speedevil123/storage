using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IModelsRepository
    {
        Task<Guid> Create(Model model);
        Task<Guid> Delete (Guid id);
        Task<List<Model>> Get();
        Task<Guid> Update(Guid id, string name, Guid categoryId);
    }
}
