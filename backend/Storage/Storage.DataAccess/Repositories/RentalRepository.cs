using Microsoft.EntityFrameworkCore;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using Storage.DataAccess;
using Storage.Infrastructure.Entities;
using Storage.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly StorageDbContext _context;

        public RentalRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Rental rental)
        {
            var rentalEntity = new RentalEntity
            {
                WorkerId = rental.WorkerId,
                ToolId = rental.ToolId,
                StartDate = rental.StartDate,
                ReturnDate = rental.ReturnDate,
                Status = rental.Status,
                Tool = rental.Tool
            };

            rentalEntity.StartDate = DateTime.Now;
            rentalEntity.Status = StatusCategories.Active;

            var workerEntity = await _context.Workers
                .FirstOrDefaultAsync(r => r.Id == rental.WorkerId);

            if (workerEntity != null)
            {
                workerEntity.Rentals.Add(rentalEntity);
                await _context.SaveChangesAsync();

                return rental.ToolId;
            }
            throw new KeyNotFoundException();
        }

        public async Task<Guid> Delete(Guid workerId, Guid toolId)
        {
            //Получаем воркера
            var workerEntity = await _context.Workers
                .Include(r => r.Rentals).FirstOrDefaultAsync(r => r.Id == workerId);

            if(workerEntity != null)
            {
                //Через workerEntity получаем rentalEntity
                var rentalEntity = workerEntity.Rentals.FirstOrDefault(r => r.ToolId == toolId);


                if (rentalEntity != null)
                {
                    workerEntity.Rentals.Remove(rentalEntity);
                    await _context.SaveChangesAsync();
                    return toolId;
                }
            }

            throw new KeyNotFoundException();
        }

        public async Task<List<Rental>> Get()
        {
            var workers = await _context.Workers.Include(r => r.Rentals)
                .ToListAsync();

            var rentalEntities = workers.SelectMany(r => r.Rentals).ToList();
            List<Rental> rentals = rentalEntities
                .Select(r => new Rental(r.WorkerId, r.ToolId, r.StartDate, r.ReturnDate, 
                r.Status, r.Worker, r.Tool)).ToList();

            return rentals;
        }

        public async Task<Guid> Update(Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, string status, Worker worker, Tool tool)
        {
            var workerEntity = await _context.Workers
                .FirstOrDefaultAsync(r => r.Id == workerId);

            //Подтягиваем рентал
            var rentalEntity = workerEntity.Rentals.FirstOrDefault(r => r.ToolId == toolId);

            if(rentalEntity != null)
            {
                //Удаляем старое состояние
                workerEntity.Rentals.Remove(rentalEntity);

                //Обновляем
                rentalEntity = new RentalEntity
                {
                    WorkerId = workerId,
                    ToolId = toolId,
                    StartDate = startDate,
                    ReturnDate = returnDate,
                    Status = status,
                    Worker = worker,
                    Tool = tool
                };

                //Добавляем новое состояние
                workerEntity.Rentals.Add(rentalEntity);
                await _context.SaveChangesAsync();
            }

            throw new KeyNotFoundException("The rental record was not found.");
        }
    }
}
