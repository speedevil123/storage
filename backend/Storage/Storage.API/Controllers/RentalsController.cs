using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;

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
    }
}
