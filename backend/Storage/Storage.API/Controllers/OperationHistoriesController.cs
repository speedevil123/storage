using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationHistoriesController : Controller
    {
        private readonly IOperationHistoryService _operationHistoryService;

        public OperationHistoriesController(IOperationHistoryService operationHistoryService)
        {
            _operationHistoryService = operationHistoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OperationHistoriesResponse>>> GetOperationHistories()
        {
            var operationHistories = await _operationHistoryService
                .GetAllOperationHistory();

            var response = operationHistories
                .Select(o => new OperationHistoriesResponse(
                    o.Id, 
                    o.OperationType,
                    string.Concat(o.Tool?.Manufacturer ?? "Unkown Manufacturer", " ", 
                        o.Tool?.Model ?? "Unknown Model"), 
                    o.Worker?.Name ?? "Unknown Worker", o.Date, o.Comment));
            
            return Ok(response);
        }
    }
}
