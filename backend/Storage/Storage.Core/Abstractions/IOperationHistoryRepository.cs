﻿using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IOperationHistoryRepository
    {
        Task<Guid> Create(OperationHistory operationHistory);
        Task<Guid> Delete(Guid workerId, Guid toolId);
        Task<List<OperationHistory>> Get();
    }
}
