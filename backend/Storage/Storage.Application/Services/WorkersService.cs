//using Storage.Core.Abstractions;
//using Storage.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Storage.Application.Services
//{
//    public class WorkersService : IWorkersService
//    {
//        private readonly IWorkersRepository _workerRepository;

//        public WorkersService(IWorkersRepository workerRepository)
//        {
//            _workerRepository = workerRepository;
//        }

//        public async Task<Guid> CreateWorker(Worker worker)
//        {
//            return await _workerRepository.Create(worker);
//        }

//        public async Task<Guid> DeleteWorker(Guid id)
//        {
//            return await _workerRepository.Delete(id);
//        }

//        public async Task<List<Worker>> GetAllWorkers()
//        {
//            return await _workerRepository.Get();
//        }

//        public async Task<Guid> UpdateWorker(Guid id, string name, string position, string department, string email, string phone, DateTime registrationDate)
//        {
//            return await _workerRepository.Update(id, name, position, department, email, phone, registrationDate);
//        }
//    }
//}
