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
                .AsNoTracking()
                .ToListAsync();

            var operationHistories = operationHistoryEntities
                .Select(o => new OperationHistory(o.Id, o.OperationType, o.ToolId, o.WorkerId, o.Date, o.Comment))
                .ToList();

            return operationHistories;
        }
    }
}
