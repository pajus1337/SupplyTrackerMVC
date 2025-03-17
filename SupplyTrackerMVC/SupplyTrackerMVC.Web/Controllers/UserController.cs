using Microsoft.AspNetCore.Mvc;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
