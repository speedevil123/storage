using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public class OperationHistoriesRepository : IOperationHistoryRepository
    {
        private readonly StorageDbContext _context;
        public OperationHistoriesRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<List<OperationHistory>> Get()
        {
            var operationHistoryEntities = await _context.OperationHistories
                .Include(e => e.Tool)
                .Include(e => e.Worker)
                .AsNoTracking()
                .ToListAsync();

            return operationHistoryEntities.Select(MapToDomain).ToList();
        }

        //Вопрос можно ли тут оставлять маппер??
        private OperationHistory MapToDomain(OperationHistoryEntity entity)
        {
            return new OperationHistory(
                entity.Id,
                entity.OperationType,
                entity.ToolId,
                entity.WorkerId,
                entity.Date,
                entity.Comment,

                //Прописываем Tool
                entity.Tool != null ? 
                new Tool(entity.Tool.Id, 
                    entity.Tool.Type, 
                    entity.Tool.Model, 
                    entity.Tool.Manufacturer, 
                    entity.Tool.Quantity, 
                    entity.Tool.IsTaken) : null,
               
                //Прописываем Worker
                entity.Worker != null ? 
                new Worker(
                    entity.Worker.Id, 
                    entity.Worker.Name, 
                    entity.Worker.Position, 
                    entity.Worker.Department, 
                    entity.Worker.Email, 
                    entity.Worker.Phone, 
                    entity.Worker.RegistrationDate) : null
            );
        }
    }
}
