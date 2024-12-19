using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IDepartmentsService
    {
        Task<Guid> CreateDepartment(Department department);
        Task<Guid> DeleteDepartment(Guid id);
        Task<List<Department>> GetAllDepartments();
        Task<Guid> UpdateDepartment(Guid id, string name, string phoneNumber, string email, string address);
    }
}
