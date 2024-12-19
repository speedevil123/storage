using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
using Storage.Core.Models;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelsController : Controller
    {
        private readonly IModelsService _modelsService;
        public ModelsController(IModelsService modelsService)
        {
            _modelsService = modelsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ModelsResponse>>> GetModels()
        {
            var models = await _modelsService.GetAllModels();

            var response = models.Select(m => new ModelsResponse(
                m.Id,
                m.Name,
                m.CategoryId,
                m.Category.Name));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateModel([FromBody] ModelsRequest request)
        {
            var model = new Model(
                Guid.NewGuid(),
                request.Name,
                request.CategoryId,
                null);

            var modelId = await _modelsService.CreateModel(model);
            return Ok(modelId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateModel(Guid id, string name, Guid categoryId)
        {
            var modelId = await _modelsService
                .UpdateModel(id, name, categoryId);
            return Ok(modelId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteModel(Guid id)
        {
            var modelId = await _modelsService.DeleteModel(id);
            return Ok(modelId);
        }
    }
}
