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

        public async Task<Guid> CreateRental(Rental rental)
        {
            return await _rentalRepository.Create(rental);
        }

        public async Task<Guid> DeleteRental(Guid workerId, Guid toolId)
        {
            return await _rentalRepository.Delete(workerId, toolId);
        }

        public async Task<List<Rental>> GetAllRentals()
        {
            return await _rentalRepository.Get();
        }

        public async Task<Guid> UpdateRental(Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, DateTime endDate, string status, int toolQuantity)
        {
            return await _rentalRepository.Update(workerId, toolId, startDate, returnDate, endDate, status, toolQuantity);
        }
    }
}
