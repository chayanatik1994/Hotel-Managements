using Microsoft.AspNetCore.Mvc;

namespace M_08_Hotel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
