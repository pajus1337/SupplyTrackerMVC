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
            var (success, errors, receiverId) = await _receiverService.AddNewReceiverAsync(model, cancellationToken);
            if (!success)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View("AddReceiverAsync", model);
            }

            return RedirectToAction("ViewReceiver", new { receiverId = receiverId });
        }

        public IActionResult ViewReceiver(int receiverId)
        {
            _receiverService.GetReceiverDetailsById(receiverId);
            return View();
        }
    }
}
