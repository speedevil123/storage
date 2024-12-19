using Microsoft.AspNetCore.Mvc;

namespace Storage.API.Controllers
{
    public class ModelsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
