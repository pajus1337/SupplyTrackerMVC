using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ReceiverController : Controller
    {
        private readonly IReceiverService _receiverService;

        public ReceiverController(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListOfActiveReceivers()
        {
            var model = _receiverService.GetAllActiveReceiversForList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddReceiver()
        {
            return View(new NewReceiverVm());
        }

        [HttpPost]
        public IActionResult AddReceiver(NewReceiverVm newReceiverModel)
        {
            var id = _receiverService.AddNewReceiver(newReceiverModel);
            return View();
        }

        public IActionResult ViewReceiver(int receiverId)
        {
            return View();
        }
    }
}
