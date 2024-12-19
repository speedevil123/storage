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
        private readonly IRentalsService _RentalsService;

        public RentalsController(IRentalsService RentalsService)
        {
            _RentalsService = RentalsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RentalsResponse>>> GetRentals()
        {
            var rentals = await _RentalsService
                .GetAllRentals();

            var response = rentals.Select(r => new RentalsResponse(
                    r.WorkerId,
                    r.ToolId,
                    r.Worker.Name,
                    r.Tool.Model.Category.Name + " " + r.Tool.Model.Name,
                    r.StartDate.ToString("g"),
                    r.ReturnDate.ToString("g"),
                    r.EndDate.ToString("g"),
                    r.Status,
                    r.ToolQuantity));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRental([FromBody] RentalsRequest request)
        {
            var rental = new Rental(
                request.WorkerId, 
                request.ToolId, 
                DateTime.Now, 
                request.ReturnDate, 
                request.EndDate, 
                "Активен", 
                null,
                null,
                request.ToolQuantity);

            var rentalToolId = await _RentalsService.CreateRental(rental);
            return Ok(rentalToolId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateRental(Guid workerId, Guid toolId, [FromBody] RentalsRequest request)
        {
            var rentalToolId = await _RentalsService
                .UpdateRental(request.WorkerId, request.ToolId, request.StartDate, request.ReturnDate,
                request.EndDate, request.Status, request.ToolQuantity);

            return Ok(rentalToolId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteRental(Guid workerId, Guid toolId)
        {
            var rentalToolId = await _RentalsService.DeleteRental(workerId, toolId);
            return Ok(rentalToolId);
        }
    }
}
