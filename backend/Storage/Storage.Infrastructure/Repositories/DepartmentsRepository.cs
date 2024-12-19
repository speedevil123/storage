using Microsoft.EntityFrameworkCore;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using Storage.DataAccess;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Repositories
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly StorageDbContext _context;
        public DepartmentsRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Department department)
        {
            var departmentEntity = new DepartmentEntity
            {
                Id = department.Id,
                Name = department.Name,
                PhoneNumber = department.PhoneNumber,
                Email = department.Email,
                Address = department.Address
            };

            await _context.Departments.AddAsync(departmentEntity);
            await _context.SaveChangesAsync();
            return department.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var departmentExists = await _context.Departments
                .AnyAsync(d => d.Id == id);

            if (!departmentExists)
            {
                throw new KeyNotFoundException("DepartmentEntity not found");
            }

            await _context.Departments
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Department>> Get()
        {
            var departmentEntities = await _context.Departments
                .AsNoTracking()
                .ToListAsync();

            return departmentEntities.Select(MapToDomain).ToList();
        }

        public async Task<Guid> Update(Guid id, string name, string phoneNumber, string email, string address)
        {
            var departmentToUpdate = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (departmentToUpdate == null)
            {
                throw new KeyNotFoundException($"DepartmentEntity with id {id} not found");
            }

            departmentToUpdate.Name = name;
            departmentToUpdate.PhoneNumber = phoneNumber;
            departmentToUpdate.Email = email;
            departmentToUpdate.Address = address;

            await _context.SaveChangesAsync();
            return id;
        }

        public static Department MapToDomain(DepartmentEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return new Department(
                entity.Id,
                entity.Name,
                entity.PhoneNumber,
                entity.Email,
                entity.Address);
        }
    }
}
