using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IServiceProvider _serviceProvider;
        public DeliveryController(IDeliveryService deliveryService, IServiceProvider serviceProvider)
        {
            _deliveryService = deliveryService;
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewDelivery()
        {
            var receiverService = ActivatorUtilities.GetServiceOrCreateInstance<IReceiverService>(_serviceProvider);
            var senderService = ActivatorUtilities.GetServiceOrCreateInstance<ISenderService>(_serviceProvider);
            var productService = ActivatorUtilities.GetServiceOrCreateInstance<IProductService>(_serviceProvider);

            var model = new NewDeliveryVm()
            {
                Products = productService.GetAllActiveProductsForSelectList(),
                Senders = senderService.GetAllActiveSendersForSelectList(),
                Receivers = receiverService.GetAllActiveReceiversForSelectList(),
               // ReceiverBranches = receiverService.GetAllActiveReceiverBranchesForSelectList(),
            };

            return View(model); 
        }

        [HttpPost]
        public IActionResult NewDelivery(NewDeliveryVm model)
        {
            return View();
        }
    }
}
