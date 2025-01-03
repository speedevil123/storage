﻿using Microsoft.EntityFrameworkCore;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using Storage.DataAccess;
using Storage.DataAccess.Repositories;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Repositories
{
    public class RentalsRepository : IRentalsRepository
    {
        private readonly StorageDbContext _context;

        public RentalsRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Rental rental)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == rental.WorkerId);
            var tool = await _context.Tools.FirstOrDefaultAsync(t => t.Id == rental.ToolId);

            if (worker == null)
            {
                throw new KeyNotFoundException($"WorkerEntity with id {rental.WorkerId} not found");
            }

            if (tool == null)
            {
                throw new KeyNotFoundException($"ToolEntity with id {rental.ToolId} not found");
            }

            var rentalEntity = new RentalEntity
            {
                Id = rental.Id,
                WorkerId = rental.WorkerId,
                ToolId = rental.ToolId,
                StartDate = rental.StartDate,
                ReturnDate = rental.ReturnDate,
                EndDate = rental.EndDate,
                Status = rental.Status,
                ToolQuantity = rental.ToolQuantity,

                //Navigation Properties
                Worker = worker,
                Tool = tool
            };

            await _context.Rentals.AddAsync(rentalEntity);
            await _context.SaveChangesAsync();

            return rentalEntity.ToolId;
        }


        public async Task<Guid> Delete(Guid id)
        {
            var rentalExists = await _context.Rentals
               .AnyAsync(r => r.Id == id);

            if (!rentalExists)
            {
                throw new KeyNotFoundException("RentalEntity not found");
            }

            await _context.Rentals
                .Where(r => r.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Rental>> Get()
        {
            var rentalEntities = await _context.Rentals
                .Include(r => r.Worker)
                    .ThenInclude(w => w.Department)
                .Include(r => r.Tool)
                    .ThenInclude(t => t.Model)
                    .ThenInclude(m => m.Category)
                .Include(r => r.Tool)
                    .ThenInclude(t => t.Manufacturer)
                .AsNoTracking()
                .ToListAsync();

            var rentals = rentalEntities
                .Select(MapToDomain).ToList();

            return rentals;
        }

        public async Task<Guid> Update(Guid id, Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, DateTime endDate, string status, int toolQuantity)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == workerId);
            var tool = await _context.Tools.FirstOrDefaultAsync(t => t.Id == toolId);
            var rentalToUpdate = await _context.Rentals
                .FirstOrDefaultAsync(r => r.Id == id);

            if (worker == null)
            {
                throw new KeyNotFoundException($"WorkerEntity with id {workerId} not found");
            }

            if (tool == null)
            {
                throw new KeyNotFoundException($"ToolEntity with id {toolId} not found");
            }

            if (rentalToUpdate == null)
            {
                throw new KeyNotFoundException($"RentalEntity with id {id} not found");
            }

            rentalToUpdate.WorkerId = workerId;
            rentalToUpdate.ToolId = toolId;
            rentalToUpdate.StartDate = startDate;
            rentalToUpdate.ReturnDate = returnDate;
            rentalToUpdate.EndDate = endDate;
            rentalToUpdate.Status = status;
            rentalToUpdate.ToolQuantity = toolQuantity;

            await _context.SaveChangesAsync();

            return toolId;
        }
        
        public static Rental MapToDomain(RentalEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Worker worker = WorkersRepository.MapToDomain(entity.Worker);
            Tool tool = ToolsRepository.MapToDomain(entity.Tool);

            return new Rental(
                entity.Id,
                entity.WorkerId,
                entity.ToolId,
                entity.StartDate,
                entity.ReturnDate,
                entity.EndDate,
                entity.Status,
                worker,
                tool,
                entity.ToolQuantity);
        }
    }
}
