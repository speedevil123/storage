using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class OperationHistoryService : IOperationHistoryService
    {
        private readonly IOperationHistoryRepository _operationHistoryRepository;

        public OperationHistoryService(IOperationHistoryRepository operationHistoryRepository)
        {
            _operationHistoryRepository = operationHistoryRepository;
        }

        public Task<Guid> CreateOperationHistory(OperationHistory operationHistory)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteOperationHistory(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OperationHistory>> GetAllOperationHistory()
        {
            throw new NotImplementedException();
        }
    }
}
