using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class OperationHistoriesService : IOperationHistoryService
    {
        private readonly IOperationHistoryRepository _operationHistoryRepository;

        public OperationHistoriesService(IOperationHistoryRepository operationHistoryRepository)
        {
            _operationHistoryRepository = operationHistoryRepository;
        }

        public async Task<Guid> CreateOperationHistory(OperationHistory operationHistory)
        {
            return await _operationHistoryRepository.Create(operationHistory);
        }

        public async Task<Guid> DeleteOperationHistory(Guid workerId, Guid toolId)
        {
            return await _operationHistoryRepository.Delete(workerId, toolId);
        }

        public async Task<List<OperationHistory>> GetAllOperationHistory()
        {
            return await _operationHistoryRepository.Get();
        }
    }
}
