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
    public class ModelsRepository : IModelsRepository
    {
        private readonly StorageDbContext _context;
        public ModelsRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Model model)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == model.CategoryId);
            
            if(category == null)
            {
                throw new KeyNotFoundException($"CategoryEntity with id {model.Id} not found");
            }

            var modelEntity = new ModelEntity
            {
                Id = model.Id,
                Name = model.Name,
                CategoryId = category.Id,
                Category = category
            };

            await _context.AddAsync(modelEntity);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var modelExists = await _context.Models
                .AnyAsync(m => m.Id == id);

            if(!modelExists)
            {
                throw new KeyNotFoundException("modelEntity not found");
            }

            await _context.Models
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Model>> Get()
        {
            var modelEntities = await _context.Models
                .Include(m => m.Category)
                .AsNoTracking()
                .ToListAsync();

            return modelEntities.Select(MapToDomain).ToList();
        }

        public async Task<Guid> Update(Guid id, string name, Guid categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            var modelToUpdate = await _context.Models.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                throw new KeyNotFoundException($"CategoryEntity with id {categoryId} not found");
            }
            if (modelToUpdate == null)
            {
                throw new KeyNotFoundException($"ModelEntity with id {id} not found");
            }

            modelToUpdate.Name = name;
            modelToUpdate.CategoryId = categoryId;
            modelToUpdate.Category = category;

            await _context.SaveChangesAsync();
            return id;
        }

        public static Model MapToDomain(ModelEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Category? category = null;
            if (entity?.Category != null)
            {
                category = new Category(
                    entity.Category.Id,
                    entity.Category.Name);
            }

            return new Model(
                entity.Id,
                entity.Name,
                entity.CategoryId,
                category);
        }
    }
}
