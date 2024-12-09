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
    public class OperationHistoryRepository : IOperationHistoryRepository
    {
        private readonly StorageDbContext _context;
        public OperationHistoryRepository(StorageDbContext context)
        {
            _context = context;
        }

        //Возможно исправление на присвоение полей с временем и типом операции
        //Брать их с уже созданного rental 
        public async Task<Guid> Create(OperationHistory operationHistory)
        {
            var operationHistoryEntity = new OperationHistoryEntity
            {
                Id = operationHistory.Id,
                OperationType = operationHistory.OperationType,
                ToolId = operationHistory.ToolId,
                WorkerId = operationHistory.WorkerId,
                Date = operationHistory.Date,
                Comment = operationHistory.Comment
            };

            var toolEntity = await _context.Tools
                .FirstOrDefaultAsync(t => t.Id == operationHistory.ToolId);

            if (toolEntity != null)
            {
                operationHistoryEntity.Tool = toolEntity;

                var workerEntity = await _context.Workers
                    .FirstOrDefaultAsync(w => w.Id == operationHistory.WorkerId);

                if(workerEntity != null)
                {
                    workerEntity.OperationHistories.Add(operationHistoryEntity);
                    await _context.SaveChangesAsync();

                    return operationHistory.ToolId;
                }
            }

            throw new KeyNotFoundException();
        }

        public async Task<Guid> Delete(Guid workerId, Guid toolId)
        {
            //Получаем воркера
            var workerEntity = await _context.Workers
                .Include(r => r.OperationHistories).FirstOrDefaultAsync(r => r.Id == workerId);

            if (workerEntity != null)
            {
                //Через workerEntity получаем operationHistoryEntity
                var operationHistoryEntity = workerEntity.OperationHistories
                    .FirstOrDefault(r => r.ToolId == toolId);


                if (operationHistoryEntity != null)
                {
                    workerEntity.OperationHistories.Remove(operationHistoryEntity);
                    await _context.SaveChangesAsync();
                    return toolId;
                }
            }

            throw new KeyNotFoundException();
        }

        //Навигационные свойства не присвоены в конструкторе
        public async Task<List<OperationHistory>> Get()
        {
            var workers = await _context.Workers.Include(r => r.OperationHistories)
                .ToListAsync();

            var operationHistoryEntities = workers.SelectMany(r => r.OperationHistories).ToList();
            List<OperationHistory> opertionHistories = operationHistoryEntities
                .Select(o => new OperationHistory(o.Id, o.OperationType, o.ToolId, o.WorkerId, o.Date, o.Comment)).ToList();

            return opertionHistories;
        }
    }
}
