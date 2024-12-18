//using Microsoft.AspNetCore.Mvc;
//using Storage.API.Contracts;
//using Storage.Application.Services;
//using Storage.Core.Models;

//namespace Storage.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class ToolsController : ControllerBase
//    {
//        private readonly IToolsService _toolsService;

//        public ToolsController(IToolsService toolsService)
//        {
//            _toolsService = toolsService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<List<ToolsResponse>>> GetTools()
//        {
//            var tools = await _toolsService.GetAllTools();

//            var response = tools.Select(t => 
//                new ToolsResponse(t.Id, t.Type, t.Model, t.Manufacturer, t.Quantity));

//            return Ok(response);
//        }

//        [HttpPost]
//        public async Task<ActionResult> CreateTool([FromBody] ToolsRequest request)
//        {
//            var tool = new Tool(Guid.NewGuid(), request.Type, request.Model, request.Manufacturer, request.Quantity);
//            var toolId = await _toolsService.CreateTool(tool);
//            return Ok(toolId);
//        }

//        [HttpPut("{id:guid}")]
//        public async Task<ActionResult<Guid>> UpdateTool(Guid id, [FromBody] ToolsRequest request)
//        {
//            var toolId = await _toolsService
//                .UpdateTool(id, request.Type, request.Model, request.Manufacturer, request.Quantity);
//            return Ok(toolId);
//        }

//        [HttpDelete("{id:guid}")]
//        public async Task<ActionResult<Guid>> DeleteTool(Guid id)
//        {
//            var toolId = await _toolsService.DeleteTool(id);
//            return Ok(toolId);
//        }

//    }
//}
