using Microsoft.EntityFrameworkCore;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using Storage.DataAccess;
using Storage.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Repositories
{
    public class PenaltiesRepository : IPenaltiesRepository
    {
        private readonly StorageDbContext _context;
        public PenaltiesRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Penalty penalty)
        {
            var rental = await _context.Rentals
                .Include(r => r.Worker)
                    .ThenInclude(w => w.Department)
                .Include(r => r.Tool)
                    .ThenInclude(t => t.Manufacturer)
                .Include(r => r.Tool)
                    .ThenInclude(t => t.Model)
                        .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(r => r.ToolId == penalty.ToolId && r.WorkerId == penalty.WorkerId);

            if (rental == null)
            {
                throw new KeyNotFoundException($"RentalEntity with workerId {penalty.WorkerId} | toolId {penalty.ToolId} not found");
            }

            var penaltyEntity = new PenaltyEntity
            {
                Id = penalty.Id,
                Fine = penalty.Fine,
                PenaltyDate = penalty.PenaltyDate,
                ToolId = penalty.ToolId,
                WorkerId = penalty.WorkerId,
                Rental = rental
            };

            await _context.Penalties.AddAsync(penaltyEntity);
            await _context.SaveChangesAsync();

            return penaltyEntity.ToolId;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var penaltyExists = await _context.Penalties
                .AnyAsync(p => p.Id == id);

            if (!penaltyExists)
            {
                throw new KeyNotFoundException("PenaltyEntity not found");
            }

            await _context.Penalties
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Penalty>> Get()
        {
            var penaltyEntities = await _context.Penalties
                .Include(p => p.Rental)
                    .ThenInclude(r => r.Worker)
                        .ThenInclude(w => w.Department)
                .Include(p => p.Rental)
                    .ThenInclude(r => r.Tool)
                        .ThenInclude(t => t.Manufacturer)
                .Include(p => p.Rental)
                    .ThenInclude(r => r.Tool)
                        .ThenInclude(t => t.Model)
                            .ThenInclude(m => m.Category)
                .AsNoTracking()
                .ToListAsync();

            return penaltyEntities.Select(MapToDomain).ToList();    
        }

        public async Task<Guid> Update(Guid id, double fine, DateTime penaltyDate, Guid toolId, Guid workerId)
        {
            var rental = await _context.Rentals
                .Include(r => r.Worker)
                    .ThenInclude(w => w.Department)
                .Include(r => r.Tool)
                    .ThenInclude(t => t.Manufacturer)
                .Include(r => r.Tool)
                    .ThenInclude(t => t.Model)
                        .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(r => r.ToolId == toolId && r.WorkerId == workerId);

            var penaltyToUpdate = await _context.Penalties.FirstOrDefaultAsync(p => p.Id == id);

            if (rental == null)
            {
                throw new KeyNotFoundException($"RentalEntity with workerId {workerId} | toolId {toolId} not found");
            }
            if (penaltyToUpdate == null)
            {
                throw new KeyNotFoundException($"PenaltyEntity with workerId {workerId} | toolId {toolId} not found");
            }

            penaltyToUpdate.PenaltyDate = penaltyDate;
            penaltyToUpdate.Fine = fine;
            penaltyToUpdate.ToolId = toolId;
            penaltyToUpdate.WorkerId = workerId;

            await _context.SaveChangesAsync();
            return toolId;
        }

        public static Penalty MapToDomain(PenaltyEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Rental? rental = RentalsRepository.MapToDomain(entity.Rental);

            return new Penalty(
                entity.Id,
                entity.Fine,
                entity.PenaltyDate,
                entity.ToolId,
                entity.WorkerId,
                rental);
        }
    }
}
