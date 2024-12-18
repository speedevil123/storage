//using Microsoft.EntityFrameworkCore;
//using Storage.Core.Abstractions;
//using Storage.Core.Models;
//using Storage.DataAccess;
//using Storage.DataAccess.Repositories;
//using Storage.Infrastructure.Entities;
//using Storage.Infrastructure.Enum;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Storage.Infrastructure.Repositories
//{
//    public class RentalRepository : IRentalRepository
//    {
//        private readonly StorageDbContext _context;

//        public RentalRepository(StorageDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<Guid> Create(Rental rental)
//        {
//            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == rental.WorkerId);
//            var tool = await _context.Tools.FirstOrDefaultAsync(t => t.Id == rental.ToolId);

//            if(worker == null)
//            {
//                throw new KeyNotFoundException($"Worker with id {rental.WorkerId} not found");
//            }

//            if (tool == null)
//            {
//                throw new KeyNotFoundException($"Tool with id {rental.ToolId} not found");
//            }

//            var rentalEntity = new RentalEntity
//            {
//                WorkerId = rental.WorkerId,
//                ToolId = rental.ToolId,
//                StartDate = rental.StartDate,
//                ReturnDate = rental.ReturnDate,
//                Status = rental.Status,

//                //Navigation Properties
//                Worker = worker,
//                Tool = tool
//            };

//            await _context.Rentals.AddAsync(rentalEntity);
//            await _context.SaveChangesAsync();

//            return rentalEntity.ToolId;
//        }


//        public async Task<Guid> Delete(Guid workerId, Guid toolId)
//        {
//            var rentalExists = await _context.Rentals
//               .AnyAsync(r => r.WorkerId == workerId && r.ToolId == toolId);

//            if (!rentalExists)
//            {
//                throw new KeyNotFoundException("RentalEntity not found");
//            }

//            await _context.Rentals
//                .Where(r => r.WorkerId == workerId && r.ToolId == toolId)
//                .ExecuteDeleteAsync();

//            return toolId;
//        }

//        public async Task<List<Rental>> Get()
//        {
//            var rentalEntities = await _context.Rentals
//                .AsNoTracking()
//                .ToListAsync();

//            var rentals = rentalEntities
//                .Select(r => new Rental(r.WorkerId, r.ToolId, r.StartDate, r.ReturnDate, r.Status))
//                .ToList();

//            return rentals;
//        }

//        public async Task<Guid> Update(Guid workerId, Guid toolId,
//            DateTime startDate, DateTime returnDate, string status)
//        {
//            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Id == workerId);
//            var tool = await _context.Tools.FirstOrDefaultAsync(t => t.Id == toolId);

//            if (worker == null)
//            {
//                throw new KeyNotFoundException($"Worker with id {workerId} not found");
//            }

//            if (tool == null)
//            {
//                throw new KeyNotFoundException($"Tool with id {toolId} not found");
//            }

//            var rentalExists = await _context.Rentals
//                .AnyAsync(r => r.WorkerId == workerId && r.ToolId == toolId);

//            if (!rentalExists)
//            {
//                throw new KeyNotFoundException("RentalEntity not found");
//            }

//            await _context.Rentals
//                .Where(r => r.WorkerId == workerId && r.ToolId == toolId)
//                .ExecuteUpdateAsync(s => s
//                .SetProperty(r => r.StartDate, r => startDate)
//                .SetProperty(r => r.ReturnDate, r => returnDate)
//                .SetProperty(r => r.Status, r => status)
//                .SetProperty(r => r.Worker, r => worker)
//                .SetProperty(r => r.Tool, r => tool));

//            return toolId;
//        }

//    }
//}
