using Microsoft.AspNetCore.Mvc;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
