using Microsoft.EntityFrameworkCore;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using Storage.DataAccess;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly StorageDbContext _context;
        public CategoriesRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Category category)
        {
            var categoryEntity = new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name
            };

            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();
            return categoryEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == id);

            if(!categoryExists)
            {
                throw new KeyNotFoundException("Category not found");
            }

            await _context.Categories
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Category>> Get()
        {
            var categoryEntities = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            return categoryEntities.Select(MapToDomain).ToList();
        }

        public async Task<Guid> Update(Guid id, string name)
        {
            var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(categoryToUpdate == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found");
            }

            categoryToUpdate.Name = name;

            await _context.SaveChangesAsync();
            return id;
        }

        public static Category MapToDomain(CategoryEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return new Category(
                entity.Id,
                entity.Name);
        }
    }
}
