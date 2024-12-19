using Microsoft.EntityFrameworkCore;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using Storage.DataAccess;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Repositories
{
    public class WorkersRepository : IWorkersRepository
    {
        private readonly StorageDbContext _context;
        public WorkersRepository(StorageDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Create(Worker worker)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == worker.DepartmentId);

            if (department == null)
            {
                throw new KeyNotFoundException($"DepartmentEntity with id {worker.DepartmentId} not found");
            }

            var workerEntity = new WorkerEntity
            {
                Id = worker.Id,
                Name = worker.Name,
                Position = worker.Position,
                Email = worker.Email,
                PhoneNumber = worker.PhoneNumber,
                RegistrationDate = worker.RegistrationDate,
                DepartmentId = department.Id,
                Department = department
            };

            await _context.Workers.AddAsync(workerEntity);
            await _context.SaveChangesAsync();

            return workerEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var workerExists = await _context.Workers
                .AnyAsync(w => w.Id == id);

            if (!workerExists)
            {
                throw new KeyNotFoundException("WorkerEntity not found");
            }

            await _context.Workers
                .Where(w => w.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Worker>> Get()
        {
            var workerEntities = await _context.Workers
                .Include(w => w.Department)
                .AsNoTracking()
                .ToListAsync();

            return workerEntities.Select(MapToDomain).ToList();
        }

        public async Task<Guid> Update(Guid id, string name, string position, string email,
            string phoneNumber, DateTime registrationDate, Guid departmentId)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
            var workerToUpdate = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);

            if (department == null)
            {
                throw new KeyNotFoundException($"DepartmentEntity with id {departmentId} not found");
            }

            if (workerToUpdate == null)
            {
                throw new KeyNotFoundException($"WorkerEntity with id {id} not found");
            }

            workerToUpdate.Id = id;
            workerToUpdate.Name = name;
            workerToUpdate.Position = position;
            workerToUpdate.Email = email;
            workerToUpdate.PhoneNumber = phoneNumber;
            workerToUpdate.RegistrationDate = registrationDate;
            workerToUpdate.DepartmentId = departmentId;
            workerToUpdate.Department = department;

            await _context.SaveChangesAsync();

            return id;
        }

        public static Worker MapToDomain(WorkerEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Department? department = null;
            if(entity.Department != null)
            {
                department = new Department(
                    entity.Department.Id,
                    entity.Department.Name,
                    entity.Department.PhoneNumber,
                    entity.Department.Email,
                    entity.Department.Address);
            }

            return new Worker(
                entity.Id,
                entity.Name,
                entity.Position,
                entity.Email,
                entity.PhoneNumber,
                entity.RegistrationDate,
                entity.DepartmentId,
                department);
        }
    }
}
