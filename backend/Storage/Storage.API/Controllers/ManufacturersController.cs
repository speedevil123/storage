using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
using Storage.Core.Models;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturersService _manufacturersService;
        public ManufacturersController(IManufacturersService manufacturersService)
        {
            _manufacturersService = manufacturersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ManufacturersResponse>>> GetManufacturers()
        {
            var manufacturers = await _manufacturersService.GetAllManufacturers();

            var response = manufacturers.Select(m => new ManufacturersResponse(
                m.Id,
                m.Name,
                m.PhoneNumber,
                m.Email,
                m.Country,
                m.PostIndex));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateManufacturer([FromBody] ManufacturersRequest request)
        {
            var manufacturer = new Manufacturer(
                request.Id,
                request.Name,
                request.phoneNumber,
                request.Email,
                request.Country,
                request.PostIndex);

            var manufacturerId = await _manufacturersService.CreateManufacturer(manufacturer);
            return Ok(manufacturerId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateManufacturer(Guid id, [FromBody] ManufacturersRequest request)
        {
            var manufacturerId = await _manufacturersService
                .UpdateManufacturer(request.Id, request.Name, request.phoneNumber, request.Email, request.Country, request.PostIndex);
            return Ok(manufacturerId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteManufacturer(Guid id)
        {
            var manufacturerId = await _manufacturersService.DeleteManufacturer(id);
            return Ok(manufacturerId);
        }
    }
}
