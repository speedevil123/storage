using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IOperationHistoryService
    {
        Task<Guid> CreateOperationHistory(OperationHistory operationHistory);
        Task<Guid> DeleteOperationHistory(Guid workerId, Guid toolId);
        Task<List<OperationHistory>> GetAllOperationHistory();
    }
}
