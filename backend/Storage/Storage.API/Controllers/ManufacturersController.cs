using Microsoft.AspNetCore.Mvc;

namespace Storage.API.Controllers
{
    public class ManufacturersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
