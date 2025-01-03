using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IRentalsService
    {
        Task<Guid> CreateRental(Rental rental);
        Task<Guid> DeleteRental(Guid id);
        Task<List<Rental>> GetAllRentals();
        Task<Guid> UpdateRental(Guid id, Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, DateTime endDate, string status, int toolQuantity);
    }
}
