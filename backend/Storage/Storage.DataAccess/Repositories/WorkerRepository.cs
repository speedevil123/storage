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
    public class WorkerRepository : IWorkerRepository
    {
        private readonly StorageDbContext _context;
        public WorkerRepository(StorageDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Create(Worker worker)
        {
            var workerEntity = new WorkerEntity
            {
                Id = worker.Id,
                Name = worker.Name,
                Position = worker.Position,
                Department = worker.Department,
                Email = worker.Email,
                Phone = worker.Phone,
                RegistrationDate = worker.RegistrationDate
            };

            await _context.Workers.AddAsync(workerEntity);
            await _context.SaveChangesAsync();
            
            return workerEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Workers
                .Where(w => w.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Worker>> Get()
        {
            var workerEntities = await _context.Workers
                .AsNoTracking()
                .ToListAsync();

            var workers = workerEntities
                .Select(w => new Worker(w.Id, w.Name, w.Position, w.Department, w.Email, w.Phone, w.RegistrationDate))
                .ToList();

            return workers;
        }

        public async Task<Guid> Update(Guid id, string name, string position, string department, string email, string phone, DateTime registrationDate)
        {
            await _context.Workers
                .Where(w => w.Id == id)
                .ExecuteUpdateAsync(w => w
                    .SetProperty(w => w.Name, w => name)
                    .SetProperty(w => w.Position, w => position)
                    .SetProperty(w => w.Department, w => department)
                    .SetProperty(w => w.Email, w => email)
                    .SetProperty(w => w.Phone, w => phone)
                    .SetProperty(w => w.RegistrationDate, w => registrationDate));

            return id;
        }
    }
}
