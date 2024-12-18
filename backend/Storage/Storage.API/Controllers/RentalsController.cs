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
                    r.Worker.Name,
                    r.Tool.Model.Category.Name + " " + r.Tool.Model.Name,
                    r.StartDate,
                    r.ReturnDate,
                    r.EndDate,
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
                request.StartDate, 
                request.ReturnDate, 
                request.EndDate, 
                request.Status, 
                null,
                null,
                request.ToolQuantity);

            var rentalToolId = await _rentalService.CreateRental(rental);
            return Ok(rentalToolId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateRental(Guid workerId, Guid toolId, [FromBody] RentalsRequest request)
        {
            var rentalToolId = await _rentalService
                .UpdateRental(request.WorkerId, request.ToolId, request.StartDate, request.ReturnDate,
                request.EndDate, request.Status, request.ToolQuantity);

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
