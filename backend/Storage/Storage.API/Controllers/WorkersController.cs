using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
using Storage.Core.Models;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkersService _workersService;

        public WorkersController(IWorkersService workersService)
        {
            _workersService = workersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkersResponse>>> GetWorkers()
        {
            var workers = await _workersService.GetAllWorkers();

            var response = workers.Select(w => new WorkersResponse(
                w.Id,
                w.Name,
                w.Position,
                w.Email,
                w.PhoneNumber,
                w.RegistrationDate,
                w.DepartmentId,
                w.Department.Name));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateWorker([FromBody] WorkersRequest request)
        {
            var worker = new Worker(
                request.Id,
                request.Name,
                request.Position,
                request.Email,
                request.PhoneNumber,
                request.RegistrationDate,
                request.DepartmentId,
                null);

            var workerId = await _workersService.CreateWorker(worker);
            return Ok(workerId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateWorker(Guid id, [FromBody] WorkersRequest request)
        {
            var workerId = await _workersService
                .UpdateWorker(
                request.Id,
                request.Name,
                request.Position,
                request.Email,
                request.PhoneNumber,
                request.RegistrationDate,
                request.DepartmentId);

            return Ok(workerId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteWorker(Guid id)
        {
            var workerId = await _workersService.DeleteWorker(id);
            return Ok(workerId);
        }
    }
}
