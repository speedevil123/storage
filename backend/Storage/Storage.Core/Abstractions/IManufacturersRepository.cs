using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IManufacturersRepository
    {
        Task<Guid> Create(Manufacturer manufacturer);
        Task<Guid> Delete(Guid id);
        Task<List<Manufacturer>> Get();
        Task<Guid> Update(Guid id, string name, string phoneNumber, string email, string country, string postIndex);
    }
}
