using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IRentalService
    {
        Task<Guid> CreateRental(Rental rental);
        Task<Guid> DeleteRental(Guid workerId, Guid toolId);
        Task<List<Rental>> GetAllRentals();
        Task<Guid> UpdateRental(Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, DateTime endDate, string status, int toolQuantity);
    }
}
