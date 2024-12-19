using Microsoft.AspNetCore.Mvc;

namespace Storage.API.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
