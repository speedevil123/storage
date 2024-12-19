using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class WorkersService : IWorkersService
    {
        private readonly IWorkersRepository _WorkersRepository;

        public WorkersService(IWorkersRepository WorkersRepository)
        {
            _WorkersRepository = WorkersRepository;
        }

        public async Task<Guid> CreateWorker(Worker worker)
        {
            return await _WorkersRepository.Create(worker);
        }

        public async Task<Guid> DeleteWorker(Guid id)
        {
            return await _WorkersRepository.Delete(id);
        }

        public async Task<List<Worker>> GetAllWorkers()
        {
            return await _WorkersRepository.Get();
        }

        public async Task<Guid> UpdateWorker(Guid id, string name, string position, string email,
            string phoneNumber, DateTime registrationDate, Guid departmentId)
        {
            return await _WorkersRepository.Update(id, name, position, email, phoneNumber, registrationDate, departmentId);
        }
    }
}
