using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Abstractions
{
    public interface IPenaltiesService
    {
        Task<Guid> CreatePenalty(Penalty penalty);
        Task<Guid> DeletePenalty(Guid id);
        Task<List<Penalty>> GetAllPenalties();
        Task<Guid> UpdatePenalty(Guid id, double fine, DateTime penaltyDate,bool isPaidOut, Guid toolId, Guid workerId);
    }
}
