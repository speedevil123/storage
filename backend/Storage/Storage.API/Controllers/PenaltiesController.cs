using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
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
        PenaltiesController(IPenaltiesService penaltiesService)
        {
            _penaltiesService = penaltiesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PenaltiesRepository>>> GetPenalties()
        {
            var penalties = await _penaltiesService.GetAllPenalties();

            var response = penalties.Select(p => new PenaltiesResponse(
                p.Id,
                p.Fine,
                p.PenaltyDate,
                p.WorkerId,
                p.ToolId,
                p.Rental.Worker.Name,
                p.Rental.Tool.Model.Category.Name + " " + p.Rental.Tool.Model.Name));

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteTool(Guid id)
        {
            var penaltyId = await _penaltiesService.DeletePenalty(id);
            return Ok(penaltyId);
        }
    }
}
