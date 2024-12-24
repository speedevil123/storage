using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using System.Linq;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _rentalsService;
        private readonly IPenaltiesService _penaltiesService;

        public RentalsController(IRentalsService rentalsService, IPenaltiesService penaltiesService)
        {
            _penaltiesService = penaltiesService;
            _rentalsService = rentalsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RentalsResponse>>> GetRentals()
        {
            var rentals = await _rentalsService
                .GetAllRentals();

            var response = rentals.Select(r => new RentalsResponse(
                    r.WorkerId,
                    r.ToolId,
                    r.Worker.Name,
                    $"{r.Tool.Model.Category.Name} {r.Tool.Model.Name} - {r.Tool.Manufacturer.Name}",
                    r.StartDate.ToString("dd.MM.yyyy hh:mm:ss"),
                    r.ReturnDate.ToString("dd.MM.yyyy hh:mm:ss"),
                    r.EndDate.ToString("dd.MM.yyyy hh:mm:ss"),
                    r.Status,
                    r.ToolQuantity));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRental([FromBody] RentalsRequest request)
        {
            var rental = new Rental(
                request.workerId, 
                request.toolId,
                Convert.ToDateTime(request.startDate), 
                request.returnDate == "" ? DateTime.MinValue : Convert.ToDateTime(request.returnDate), 
                Convert.ToDateTime(request.endDate), 
                request.status, 
                null,
                null,
                request.toolQuantity);

            var rentalToolId = await _rentalsService.CreateRental(rental);
            return Ok(rentalToolId);
        }

        [HttpPut("{workerId:guid}/{toolId:guid}")]
        public async Task<ActionResult<Guid>> UpdateRental(Guid workerId, Guid toolId, [FromBody] RentalsRequest request)
        {
            if(request.status == "Просрочено")
            {
                var daysDelay = (Convert.ToDateTime(request.returnDate) - Convert.ToDateTime(request.endDate)).TotalDays;
                var penalty = new Penalty(
                    Guid.NewGuid(),
                     Math.Round(daysDelay * 50.0,4),
                     DateTime.Now,
                     true,
                     toolId,
                     workerId,
                     null);

                var penaltyToolId = await _penaltiesService.CreatePenalty(penalty);
            }
            var rentalToolId = await _rentalsService
                .UpdateRental(
                workerId, 
                toolId, 
                Convert.ToDateTime(request.startDate), 
                Convert.ToDateTime(request.returnDate),
                Convert.ToDateTime(request.endDate), 
                request.status, 
                request.toolQuantity);

            return Ok(rentalToolId);
        }

        [HttpDelete("{workerId:guid}/{toolId:guid}")]
        public async Task<ActionResult<Guid>> DeleteRental(Guid workerId, Guid toolId)
        {
            var rentalToolId = await _rentalsService.DeleteRental(workerId, toolId);
            return Ok(rentalToolId);
        }
    }
}
