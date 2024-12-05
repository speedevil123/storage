using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    internal interface IRentalRepository
    {
        Task<Guid> Create(Rental rental);
        Task<Guid> Delete(Guid workerId, Guid toolId);
        Task<List<Rental>> Get();
        Task<Guid> Update(Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate,string status, Worker worker,Tool tool);
    }
}
