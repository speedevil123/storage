using Storage.Core.Abstractions;
using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Services
{
    public class PenaltiesService : IPenaltiesService
    {
        private readonly IPenaltiesRepository _penaltiesRepository;
        public PenaltiesService(IPenaltiesRepository penaltiesRepository)
        {
            _penaltiesRepository = penaltiesRepository;
        }
        public async Task<Guid> CreatePenalty(Penalty penalty)
        {
            return await _penaltiesRepository.Create(penalty);
        }

        public async Task<Guid> DeletePenalty(Guid id)
        {
            return await _penaltiesRepository.Delete(id);
        }

        public async Task<List<Penalty>> GetAllPenalties()
        {
            return await _penaltiesRepository.Get();
        }

        public async Task<Guid> UpdatePenalty(Guid id, double fine, DateTime penaltyDate, Guid toolId, Guid workerId)
        {
            return await _penaltiesRepository.Update(id, fine, penaltyDate, toolId, workerId);
        }
    }
}
