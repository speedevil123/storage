using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class RentalsService : IRentalsService
    {
        private readonly IRentalsRepository _RentalsRepository;

        public RentalsService(IRentalsRepository RentalsRepository)
        {
            _RentalsRepository = RentalsRepository;
        }

        public async Task<Guid> CreateRental(Rental rental)
        {
            return await _RentalsRepository.Create(rental);
        }

        public async Task<Guid> DeleteRental(Guid id)
        {
            return await _RentalsRepository.Delete(id);
        }

        public async Task<List<Rental>> GetAllRentals()
        {
            return await _RentalsRepository.Get();
        }

        public async Task<Guid> UpdateRental(Guid id, Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, DateTime endDate, string status, int toolQuantity)
        {
            return await _RentalsRepository.Update(id, workerId, toolId, startDate, returnDate, endDate, status, toolQuantity);
        }
    }
}
