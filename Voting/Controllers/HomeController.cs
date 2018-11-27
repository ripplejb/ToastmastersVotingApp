using Microsoft.AspNetCore.Mvc;

namespace Voting.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}