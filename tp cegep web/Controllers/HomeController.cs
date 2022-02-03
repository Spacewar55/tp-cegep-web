using Microsoft.AspNetCore.Mvc;

namespace tp_cegep_web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
