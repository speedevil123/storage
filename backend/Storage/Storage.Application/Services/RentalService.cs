using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public Task<Guid> CreateRental(Rental rental)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteRental(Guid workerId, Guid toolId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Rental>> GetAllRentals()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateRental(Guid workerId, Guid toolId, DateTime startDate, DateTime returnDate, string status, Worker worker, Tool tool)
        {
            throw new NotImplementedException();
        }
    }
}
