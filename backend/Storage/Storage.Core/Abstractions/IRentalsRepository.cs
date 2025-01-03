using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IRentalsRepository
    {
        Task<Guid> Create(Rental rental);
        Task<Guid> Delete(Guid id);
        Task<List<Rental>> Get();
        Task<Guid> Update(Guid id, Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, DateTime endDate, string status, int toolQuantity);
    }
}
