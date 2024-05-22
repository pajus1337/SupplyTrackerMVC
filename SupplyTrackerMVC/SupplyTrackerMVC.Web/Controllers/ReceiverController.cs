using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
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
        public async Task<IActionResult> AddReceiver(NewReceiverVm model, CancellationToken cancellationToken)
        {
            var (success, errors, receiverId) = await _receiverService.AddReceiverAsync(model, cancellationToken);
            if (!success)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View("AddReceiver", model);
            }

            return RedirectToAction("ViewReceiver", new { receiverId = receiverId });
        }

        public async Task<IActionResult> ViewReceiver(int receiverId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.GetReceiverDetailsByIdAsync(receiverId, cancellationToken);
            return View(serviceResponse.Data);
        }
    }
}
