using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
using Storage.Core.Models;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriesResponse>>> GetCategories()
        {
            var categories = await _categoriesService.GetAllCategories();

            var response = categories.Select(c => new CategoriesResponse(
                c.Id,
                c.Name));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] CategoriesRequest request)
        {
            var category = new Category(
                Guid.NewGuid(),
                request.Name);

            await _categoriesService.CreateCategory(category);
            return Ok(category);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateCategory(Guid id, [FromBody] CategoriesRequest request)
        {
            var categoryId = await _categoriesService
                .UpdateCategory(id, request.Name);

            return Ok(categoryId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteCategory(Guid id)
        {
            var categoryId = await _categoriesService .DeleteCategory(id);
            return Ok(categoryId);
        }
    }
}
