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
        public async Task<IActionResult> ListOfActiveReceivers(CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.GetReceiversForListAsysnc(cancellationToken);

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public IActionResult AddReceiver()
        {
            return View(new NewReceiverVm());
        }


        [HttpPost]
        public async Task<IActionResult> AddReceiver(NewReceiverVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.AddReceiverAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                if (serviceResponse.ErrorMessage != null)
                {
                    foreach (var error in serviceResponse.ErrorMessage)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                return View("AddReceiver", model);
            }

            return RedirectToAction("ViewReceiver", new { receiverId = serviceResponse.ObjectId });
        }

        public async Task<IActionResult> ViewReceiver(int receiverId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.GetReceiverDetailsByIdAsync(receiverId, cancellationToken);
            return View(serviceResponse.Data);
        }
    }
}
