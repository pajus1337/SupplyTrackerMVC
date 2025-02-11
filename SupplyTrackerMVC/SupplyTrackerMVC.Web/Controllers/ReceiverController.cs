using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ReceiverController : BaseController
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
            var serviceResponse = await _receiverService.GetReceiversForListAsync(cancellationToken);

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

        [HttpGet]
        public async Task<IActionResult> ViewReceiver(int receiverId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.GetReceiverDetailsByIdAsync(receiverId, cancellationToken);
            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReceiver(int receiverId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.GetReceiverForEditAsync(receiverId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReceiver(UpdateReceiverVm model, CancellationToken cancellationToken)
        {
            _receiverService.
        }

        [HttpGet]
        public async Task<IActionResult> AddReceiverBranch(CancellationToken cancellationToken)
        {
            var model = await _receiverService.PrepareNewReceiverBranchVm(cancellationToken);

                return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReceiverBranch(NewReceiverBranchVm model, CancellationToken cancellationToken)
        {
            // HACK: WIP AddReceiverBranch Prototype
            var serviceResponse  = await _receiverService.AddReceiverBranchAsync(model, cancellationToken);

            if (!serviceResponse.Success)
            {
                if (serviceResponse.ErrorMessage != null)
                {
                    foreach (var error in serviceResponse.ErrorMessage)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                return View("AddReceiverBranch", model);
            }

            return RedirectToAction("ViewReceiverBranch", new { receiverBranchId = serviceResponse.ObjectId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewReceiverBranch(int receiverBranchId, CancellationToken cancellationToken)
        {
           var serviceResponse = await _receiverService.GetReceiverBranchDetailsAsync(receiverBranchId, cancellationToken);

            // TODO: Finish !success implementation case
            if (!serviceResponse.Success)
            {

            }

            return View(serviceResponse.Data);
        }
    }
}
