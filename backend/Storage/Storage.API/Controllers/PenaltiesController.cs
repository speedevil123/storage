using Microsoft.AspNetCore.Mvc;

namespace Storage.API.Controllers
{
    public class PenaltiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
