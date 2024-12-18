//using Microsoft.EntityFrameworkCore;
//using Storage.Core.Models;
//using Storage.Infrastructure.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Storage.DataAccess.Repositories
//{
//    public class ToolsRepository : IToolsRepository
//    {
//        private readonly StorageDbContext _context;
//        public ToolsRepository(StorageDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<Guid> Create(Tool tool)
//        {
//            var toolEntity = new ToolEntity
//            {
//                Id = tool.Id,
//                Type = tool.Type,
//                Model = tool.Model,
//                Manufacturer = tool.Manufacturer,
//                Quantity = tool.Quantity
//            };

//            await _context.Tools.AddAsync(toolEntity);
//            await _context.SaveChangesAsync();

//            return toolEntity.Id;
//        }

//        public async Task<Guid> Delete(Guid id)
//        {
//            var toolExists = await _context.Tools
//                .AnyAsync(w => w.Id == id);

//            if (!toolExists)
//            {
//                throw new KeyNotFoundException("ToolEntity not found");
//            }

//            await _context.Tools
//                .Where(t => t.Id == id)
//                .ExecuteDeleteAsync();

//            return id;
//        }

//        public async Task<List<Tool>> Get()
//        {
//            var toolEntities = await _context.Tools
//                .AsNoTracking()
//                .ToListAsync();

//            var tools = toolEntities
//                .Select(t => new Tool(t.Id, t.Type, t.Model, t.Manufacturer, t.Quantity))
//                .ToList();

//            return tools;
//        }

//        public async Task<Guid> Update(Guid id, string type, string model, string manufacturer, int quantity)
//        {
//            var toolExists = await _context.Tools
//                .AnyAsync(w => w.Id == id);

//            if (!toolExists)
//            {
//                throw new KeyNotFoundException("ToolEntity not found");
//            }

//            await _context.Tools
//                .Where(t => t.Id == id)
//                .ExecuteUpdateAsync(s => s
//                    .SetProperty(t => t.Type, t => type)
//                    .SetProperty(t => t.Model, t => model)
//                    .SetProperty(t => t.Manufacturer, t => manufacturer)
//                    .SetProperty(t => t.Quantity, t => quantity));

//            return id;
//        }
//    }
//}
