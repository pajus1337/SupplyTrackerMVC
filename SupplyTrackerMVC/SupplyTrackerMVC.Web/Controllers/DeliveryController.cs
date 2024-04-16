using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewDelivery()
        {
            return View(new NewDeliveryVm()); 
        }

        [HttpPost]
        public IActionResult NewDelivery(NewDeliveryVm model)
        {
            return View();
        }
    }
}
