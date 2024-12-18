﻿using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IManufacturersService
    {
        Task<Guid> CreateManufacturer(Manufacturer manufacturer);
        Task<Guid> DeleteManufacturer(Guid id);
        Task<List<Manufacturer>> GetAllManufacturers();
        Task<Guid> UpdateManufacturer(Guid id, string name, string phoneNumber, string email, string country, string postIndex);
    }
}
