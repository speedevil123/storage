using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class CategoriesService : ICategoriesService
    {
        public readonly ICategoriesRepository _categoriesRepository;
        public CategoriesService(ICategoriesRepository categoriesRepository) 
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<Guid> CreateCategory(Category category)
        {
            return await _categoriesRepository.Create(category);
        }

        public async Task<Guid> DeleteCategory(Guid categoryId)
        {
            return await _categoriesRepository.Delete(categoryId);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoriesRepository.Get();
        }

        public Task<Guid> UpdateCategory(Guid id, string Name)
        {
            return _categoriesRepository.Update(id, Name);
        }
    }
}
