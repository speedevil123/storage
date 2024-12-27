using Microsoft.AspNetCore.Mvc;
using Storage.API.Contracts;
using Storage.Core.Abstractions;
using Storage.Core.Models;
using System.Runtime.InteropServices;

namespace Storage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentsService;

        public DepartmentsController(IDepartmentsService departmentService)
        {
            _departmentsService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentsResponse>>> GetDepartments()
        {
            var departments = await _departmentsService.GetAllDepartments();

            var response = departments.Select(d => new DepartmentsResponse(
                d.Id,
                d.Name,
                d.PhoneNumber,
                d.Email,
                d.Address));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDepartments([FromBody] DepartmentsRequest request)
        {
            var department = new Department(
                request.Id,
                request.Name,
                request.phoneNumber,
                request.Email,
                request.Address);

            var departmentId = await _departmentsService.CreateDepartment(department);
            return Ok(departmentId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateDepartment(Guid id, [FromBody] DepartmentsRequest request)
        {
            var departmentId = await _departmentsService
                .UpdateDepartment(id, request.Name, request.phoneNumber, request.Email, request.Address);
            return Ok(departmentId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteDepartment(Guid id)
        {
            var departmentId = await _departmentsService.DeleteDepartment(id);
            return Ok(departmentId);
        }
    }
}
