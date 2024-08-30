using Microsoft.AspNetCore.Mvc;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
