using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Application.Services;
using Storage.Core.Models;

namespace Storage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly IToolsService _toolsService;

        public ToolsController(IToolsService toolsService)
        {
            _toolsService = toolsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToolsResponse>>> GetTools()
        {
            var tools = await _toolsService.GetAllTools();

            var response = tools.Select(t => new ToolsResponse(t.Id, t.Name, t.Model, t.Manufacturer, t.Stock));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTool([FromBody] ToolsRequest request)
        {
            var tool = new Tool(Guid.NewGuid(), request.Name, request.Model, request.Manufacturer, request.Stock);
            var toolId = await _toolsService.CreateTool(tool);
            return Ok(toolId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateTools(Guid id, [FromBody] ToolsRequest request)
        {
            var toolId = await _toolsService.UpdateTool(id, request.Name, request.Model, request.Manufacturer, request.Stock);
            return Ok(toolId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteTool(Guid id)
        {
            var toolId = await _toolsService.DeleteTool(id);
            return Ok(toolId);
        }

    }
}
