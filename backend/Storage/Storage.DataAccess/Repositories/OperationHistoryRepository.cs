using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Repositories
{
    public class OperationHistoryRepository : IOperationHistoryRepository
    {
        public Task<Guid> Create(OperationHistory operationHistory)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OperationHistory>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
