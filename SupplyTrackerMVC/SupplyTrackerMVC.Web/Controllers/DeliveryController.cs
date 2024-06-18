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
            var model = _deliveryService.PrepareNewDeliveryViewModel();
            return View(model); 
        }

        [HttpPost]
        public IActionResult NewDelivery(NewDeliveryVm model)
        {
            if (ModelState.IsValid)
            {
                return View("TestOK");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GetReceiverBranches(int receiverId)
        {
            var branches = _deliveryService.GetReceiverBranchesByReceiverId(receiverId);
            return Json(branches);
        }
    }
}
