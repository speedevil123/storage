using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IWorkersService
    {
        Task<Guid> CreateWorker(Worker worker);
        Task<Guid> DeleteWorker(Guid id);
        Task<List<Worker>> GetAllWorkers();
        Task<Guid> UpdateWorker(Guid id, string name, string position,
            string department, string email, string phone, DateTime registrationDate);
    }
}
