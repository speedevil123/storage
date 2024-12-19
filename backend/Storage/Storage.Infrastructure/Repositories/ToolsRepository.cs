using Microsoft.EntityFrameworkCore;
using Storage.Core.Models;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DataAccess.Repositories
{
    public class ToolsRepository : IToolsRepository
    {
        private readonly StorageDbContext _context;
        public ToolsRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Tool tool)
        {
            //Вложенные include?
            var model = await _context.Models.Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == tool.ModelId);
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == tool.ManufacturerId);

            var toolExists = await _context.Tools
                .FirstOrDefaultAsync(t => t.ModelId == tool.ModelId && t.ManufacturerId == tool.ManufacturerId);

            if (model == null)
            {
                throw new KeyNotFoundException($"ModelEntity with id {tool.ModelId} not found");
            }
            if (manufacturer == null)
            {
                throw new KeyNotFoundException($"ManufacturerEntity with id {tool.ManufacturerId} not found");
            }
            
            if(toolExists == null)
            {
                var toolEntity = new ToolEntity
                {
                    Id = tool.Id,
                    ModelId = tool.ModelId,
                    ManufacturerId = tool.ManufacturerId,
                    Quantity = tool.Quantity,
                    Model = model,
                    Manufacturer = manufacturer
                };

                await _context.Tools.AddAsync(toolEntity);
                await _context.SaveChangesAsync();
                return toolEntity.Id;
            }

            toolExists.Quantity += tool.Quantity;

            await _context.SaveChangesAsync();

            return toolExists.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var toolExists = await _context.Tools
                .AnyAsync(w => w.Id == id);

            if (!toolExists)
            {
                throw new KeyNotFoundException("ToolEntity not found");
            }

            await _context.Tools
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Tool>> Get()
        {
            var toolEntities = await _context.Tools
                .Include(t => t.Model)
                    .ThenInclude(m => m.Category)
                .Include(t => t.Manufacturer)
                .AsNoTracking()
                .ToListAsync();

            return toolEntities.Select(MapToDomain).ToList();
        }

        public async Task<Guid> Update(Guid id, int Quantity, Guid modelId, Guid manufacturerId)
        {
            var model = await _context.Models.Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == modelId);
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == manufacturerId);
            var toolToUpdate = await _context.Tools.FirstOrDefaultAsync(t => t.Id == id);

            if (model == null)
            {
                throw new KeyNotFoundException($"ModelEntity with id {modelId} not found");
            }

            if (manufacturer == null)
            {
                throw new KeyNotFoundException($"ManufacturerEntity with id {manufacturerId} not found");
            }

            if (toolToUpdate == null)
            {
                throw new KeyNotFoundException($"ToolEntity with id {id} not found");
            }

            toolToUpdate.Quantity = Quantity;
            toolToUpdate.ModelId = modelId;
            toolToUpdate.ManufacturerId = manufacturerId;
            toolToUpdate.Model = model;
            toolToUpdate.Manufacturer = manufacturer;

            await _context.SaveChangesAsync();
            return id;
        }

        public static Tool MapToDomain(ToolEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Category? category = null;
            if(entity.Model?.Category != null)
            {
                category = new Category(
                    entity.Model.Category.Id,
                    entity.Model.Category.Name);
            }

            Model? model = null;
            
            if(entity.Model != null)
            {
                model = new Model(
                    entity.Model.Id,
                    entity.Model.Name,
                    entity.Model.CategoryId,
                    category);
            }

            Manufacturer? manufacturer = null;
            if(entity.Manufacturer != null)
            {
                manufacturer = new Manufacturer(
                    entity.Manufacturer.Id,
                    entity.Manufacturer.Name,
                    entity.Manufacturer.PhoneNumber,
                    entity.Manufacturer.Email,
                    entity.Manufacturer.Country,
                    entity.Manufacturer.PostIndex);
            }

            return new Tool(
                entity.Id,
                entity.ModelId,
                entity.ManufacturerId,
                entity.Quantity,
                model,
                manufacturer);
        }
    }
}
