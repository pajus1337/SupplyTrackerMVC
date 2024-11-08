using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.Common;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class AdminController() : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }       
    }
}
