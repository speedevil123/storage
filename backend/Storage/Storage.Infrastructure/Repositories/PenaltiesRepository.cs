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


        public async Task<Penalty> GetPenaltyById(Guid id)
        {
            var penalty = await _context.Penalties
                .Include(p => p.Rental)
                    .ThenInclude(r => r.Worker)
                .Include(p => p.Rental)
                    .ThenInclude(r => r.Tool)
                        .ThenInclude(t => t.Model)
                            .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(p => p.RentalId == id);

            if(penalty == null)
            {
                throw new KeyNotFoundException($"PenaltyEntity with id {id} not found");
            }

            return (MapToDomain(penalty));
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
                .FirstOrDefaultAsync(r => r.Id == penalty.Id);

            if (rental != null)
            {
                throw new KeyNotFoundException($"RentalEntity with id {penalty.Id} already exsists");
            }

            var penaltyEntity = new PenaltyEntity
            {
                Id = penalty.Id,
                Fine = penalty.Fine,
                PenaltyDate = penalty.PenaltyDate,
                IsPaidOut = penalty.IsPaidOut,

                RentalId = penalty.RentalId,
                Rental = rental
            };

            await _context.Penalties.AddAsync(penaltyEntity);
            await _context.SaveChangesAsync();

            return penaltyEntity.Id;
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

        public async Task<Guid> Update(Guid id, double fine, DateTime penaltyDate,bool isPaidOut, Guid rentalId)
        {
            var rental = await _context.Rentals
                .Include(r => r.Worker)
                    .ThenInclude(w => w.Department)
                .Include(r => r.Tool)
                    .ThenInclude(t => t.Manufacturer)
                .Include(r => r.Tool)
                    .ThenInclude(t => t.Model)
                        .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(r => r.Id == rentalId);

            var penaltyToUpdate = await _context.Penalties.FirstOrDefaultAsync(p => p.Id == id);

            if (rental == null)
            {
                throw new KeyNotFoundException($"RentalEntity with id {rentalId} not found");
            }
            if (penaltyToUpdate == null)
            {
                throw new KeyNotFoundException($"PenaltyEntity with id {id} not found");
            }

            penaltyToUpdate.PenaltyDate = penaltyDate;
            penaltyToUpdate.IsPaidOut = isPaidOut;
            penaltyToUpdate.Fine = fine;
            penaltyToUpdate.RentalId = rentalId;

            await _context.SaveChangesAsync();
            return id;
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
                entity.IsPaidOut,
                entity.RentalId,
                rental);
        }
    }
}
