using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IDepartmentsRepository _departmentsRepository;
        public DepartmentsService(IDepartmentsRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        public async Task<Guid> CreateDepartment(Department department)
        {
            return await _departmentsRepository.Create(department);
        }

        public async Task<Guid> DeleteDepartment(Guid id)
        {
            return await _departmentsRepository.Delete(id);
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _departmentsRepository.Get();
        }

        public async Task<Guid> UpdateDepartment(Guid id, string name, string phoneNumber, string email, string address)
        {
            return await _departmentsRepository.Update(id, name, phoneNumber, email, address);
        }
    }
}
