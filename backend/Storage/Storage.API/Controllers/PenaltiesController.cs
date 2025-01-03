using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using Storage.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PenaltiesController : ControllerBase
    {
        private readonly IPenaltiesService _penaltiesService;
        public PenaltiesController(IPenaltiesService penaltiesService)
        {
            _penaltiesService = penaltiesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PenaltiesResponse>>> GetPenalties()
        {
            var penalties = await _penaltiesService.GetAllPenalties();

            var response = penalties.Select(p => new PenaltiesResponse(
                p.Id,
                p.Fine,
                p.PenaltyDate,
                p.IsPaidOut,
                p.Rental.WorkerId,
                p.Rental.ToolId,
                p.Rental.Worker.Name,
                p.Rental.Tool.Model.Category.Name + " " + p.Rental.Tool.Model.Name));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PenaltiesResponse>> GetPenaltyById(Guid id)
        {
            var penalty = await _penaltiesService.GetPenaltyById(id);

            var response = new PenaltiesResponse(
                penalty.Id,
                penalty.Fine,
                penalty.PenaltyDate,
                penalty.IsPaidOut,
                penalty.Rental.WorkerId,
                penalty.Rental.ToolId,
                penalty.Rental.Worker.Name,
                $"{penalty.Rental.Tool.Model.Category.Name} {penalty.Rental.Tool.Model.Name}"
            );

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdatePenalty(Guid id, [FromBody] PenaltiesRequest request)
        {
            var penaltyId = await _penaltiesService
                .UpdatePenalty(id, request.Fine, request.PenaltyDate, request.IsPaidOut, request.ToolId, request.WorkerId);
            return Ok(penaltyId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeletePenalty(Guid id)
        {
            var penaltyId = await _penaltiesService.DeletePenalty(id);
            return Ok(penaltyId);
        }
    }
}
