using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface ICategoriesService
    {
        Task<Guid> CreateCategory(Category category);
        Task<Guid> DeleteCategory(Guid categoryId);
        Task<List<Category>> GetAllCategories();
        Task<Guid> UpdateCategory(Guid id, string Name);
    }
}
