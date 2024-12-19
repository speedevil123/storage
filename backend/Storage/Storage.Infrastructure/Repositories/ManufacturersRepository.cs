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
    public class ManufacturersRepository : IManufacturersRepository
    {
        private readonly StorageDbContext _context;
        public ManufacturersRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Manufacturer manufacturer)
        {
            var manufacturerEntity = new ManufacturerEntity
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                PhoneNumber = manufacturer.PhoneNumber,
                Email = manufacturer.Email,
                Country = manufacturer.Country,
                PostIndex = manufacturer.PostIndex
            };

            await _context.Manufacturers.AddAsync(manufacturerEntity);
            await _context.SaveChangesAsync();

            return manufacturerEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var manufacturerExists = await _context.Manufacturers
                .AnyAsync(m => m.Id == id);

            if (!manufacturerExists)
            {
                throw new KeyNotFoundException("ManufacturerEntity not found");
            }

            await _context.Manufacturers
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Manufacturer>> Get()
        {
            var manufacturerEntities = await _context.Manufacturers
                .AsNoTracking()
                .ToListAsync();

            return manufacturerEntities.Select(MapToDomain).ToList();
        }

        public async Task<Guid> Update(Guid id, string name, string phoneNumber, string email, string country, string postIndex)
        {
            var manufacturertoUpdate = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == id);

            if (manufacturertoUpdate == null)
            {
                throw new KeyNotFoundException($"ManufacturerEntity with id {id} not found");
            }

            manufacturertoUpdate.Name = name;
            manufacturertoUpdate.PhoneNumber = phoneNumber;
            manufacturertoUpdate.Email = email;
            manufacturertoUpdate.Country = country;
            manufacturertoUpdate.PostIndex = postIndex;

            await _context.SaveChangesAsync();
            return id;
        }

        public static Manufacturer MapToDomain(ManufacturerEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return new Manufacturer(
                entity.Id,
                entity.Name,
                entity.PhoneNumber,
                entity.Email,
                entity.Country,
                entity.PostIndex);
        }
    }
}
