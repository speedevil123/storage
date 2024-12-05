using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;

        public WorkerService(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public Task<Guid> CreateWorker(Worker worker)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteWorker(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Worker>> GetAllWorkers()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateWorker(Guid id, string name, string position, string department, string email, string phone, DateTime registrationDate)
        {
            throw new NotImplementedException();
        }
    }
}
