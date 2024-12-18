using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
using Storage.Core.Models;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        
        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RentalsResponse>>> GetRentals()
        {
            var rentals = await _rentalService
                .GetAllRentals();

            var response = rentals
                .Select(r => new RentalsResponse(
                    r.WorkerId,
                    r.ToolId,
                    r.Worker?.Name ?? "Unknown Worker",
                    string.Concat(r.Tool?.Manufacturer ?? "Unkown Manufacturer", " ", r.Tool?.Model ?? "Unknown Model"),
                    r.StartDate,
                    r.ReturnDate,
                    r.Status
                    ));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRental([FromBody] RentalsRequest request)
        {
            var rental = new Rental(request.workerId, request.toolId, request.startDate, request.returnDate, request.status);
            var rentalToolId = await _rentalService.CreateRental(rental);
            return Ok(rentalToolId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateRental(Guid workerId, Guid toolId, [FromBody] RentalsRequest request)
        {
            var rentalToolId = await _rentalService
                .UpdateRental(workerId, toolId, request.startDate, request.returnDate, request.status);
            return Ok(rentalToolId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteRental(Guid workerId, Guid toolId)
        {
            var rentalToolId = await _rentalService.DeleteRental(workerId, toolId);
            return Ok(rentalToolId);
        }
    }
}
