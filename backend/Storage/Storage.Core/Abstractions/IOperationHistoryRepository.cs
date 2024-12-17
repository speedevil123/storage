using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IOperationHistoryRepository
    {
        Task<List<OperationHistory>> Get();
    }
}
