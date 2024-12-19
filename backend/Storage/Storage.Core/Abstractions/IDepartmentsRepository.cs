using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IDepartmentsRepository
    {
        Task<Guid> Create(Department department);
        Task<Guid> Delete(Guid id);
        Task<List<Department>> Get();
        Task<Guid> Update(Guid id, string name, string phoneNumber, string email, string address);
    }
}
