using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class ManufacturersService : IManufacturersService
    {
        private readonly IManufacturersRepository _manufacturersRepository;

        public ManufacturersService(IManufacturersRepository manufacturersRepository)
        {
            _manufacturersRepository = manufacturersRepository;
        }

        public async Task<Guid> CreateManufacturer(Manufacturer manufacturer)
        {
            return await _manufacturersRepository.Create(manufacturer);
        }

        public async Task<Guid> DeleteManufacturer(Guid id)
        {
            return await _manufacturersRepository.Delete(id);
        }

        public async Task<List<Manufacturer>> GetAllManufacturers()
        {
            return await _manufacturersRepository.Get();
        }

        public async Task<Guid> UpdateManufacturer(Guid id, string name, string phoneNumber, string email, string country, string postIndex)
        {
            return await _manufacturersRepository.Update(id, name, phoneNumber, email, country, postIndex);
        }
    }
}
