using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    internal interface IOperationHistoryService
    {
        Task<Guid> CreateOperationHistory(OperationHistory operationHistory);
        Task<Guid> DeleteOperationHistory(Guid id);
        Task<List<OperationHistory>> GetAllOperationHistory();
    }
}
