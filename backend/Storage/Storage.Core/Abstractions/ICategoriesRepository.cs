using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface ICategoriesRepository
    {
        Task<Guid> Create(Category category);
        Task Delete(Guid id);
        Task<List<Category>> Get();
        Task<Guid> Update(Guid id, string name);
    }
}
