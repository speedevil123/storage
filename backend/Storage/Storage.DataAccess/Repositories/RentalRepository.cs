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

            if(rental.StartDate == new DateTime())
            {
                rentalEntity.StartDate = DateTime.Now;
                rentalEntity.Status = StatusCategories.Active;
            }

            var worker = await _context.Workers
                .FirstOrDefaultAsync(r => r.Id ==  rental.WorkerId);

            //Проверка на нулл
            worker.Rentals.Add(rentalEntity);
            await _context.SaveChangesAsync();

            return rental.ToolId;
        }

        public async Task<Guid> Delete(Guid workerId, Guid toolId)
        {
            var rentalEntity = await _context.Rentals
                .FirstOrDefaultAsync(r => r.WorkerId == workerId && r.ToolId == toolId);

            if (rentalEntity != null)
            {
                _context.Rentals.Remove(rentalEntity);
                await _context.SaveChangesAsync();
                return rentalEntity.Id;
            }

            throw new KeyNotFoundException("The rental record was not found.");
        }

        public async Task<List<Rental>> Get()
        {
            var rentalEntities = await _context.Rentals
                .AsNoTracking()
                .ToListAsync();

            var rentals = rentalEntities
                .Select(r => new Rental(r.WorkerId, r.ToolId, r.StartDate, r.ReturnDate, r.Status, r.Worker, r.Tool))
                .ToList();

            return rentals;
        }

        public async Task<Guid> Update(Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, string status, Worker worker, Tool tool)
        {
            var rentalEntity = await _context.Rentals
                .FirstOrDefaultAsync(r => r.WorkerId == workerId && r.ToolId == toolId);

            if (rentalEntity != null)
            {
                rentalEntity.StartDate = startDate;
                rentalEntity.ReturnDate = returnDate;
                rentalEntity.Status = status;

                await _context.SaveChangesAsync();
                return rentalEntity.Id;
            }

            throw new KeyNotFoundException("The rental record was not found.");
        }
    }
}
