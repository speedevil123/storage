using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IWorkersRepository
    {
        Task<Guid> Create(Worker worker);
        Task<Guid> Delete(Guid id);
        Task<List<Worker>> Get();
        Task<Guid> Update(Guid id, string name, string position,
            string department, string email, string phone, DateTime registrationDate);
    }
}
