using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IPenaltiesRepository
    {
        Task<Guid> Create(Penalty penalty);
        Task<Guid> Delete(Guid id);
        Task<List<Penalty>> Get();
        Task<Guid> Update(Guid id, double fine, DateTime penaltyDate,bool isPaidOut, Guid toolId, Guid workerId);
    }
}
