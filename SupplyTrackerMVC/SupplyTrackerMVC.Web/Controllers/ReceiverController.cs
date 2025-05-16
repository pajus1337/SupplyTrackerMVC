using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ReceiverController : BaseController
    {
        private readonly IReceiverService _receiverService;
        private readonly IContactService _contactService;

        public ReceiverController(IReceiverService receiverService, IContactService contactService)
        {
            _receiverService = receiverService;
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewReceiverList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.GetReceiversForListAsync(5, 1, "", cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ViewReceiverList(int pageSize, CancellationToken cancellationToken, int pageNo = 1, string searchString = "")
        {
            var serviceResponse = await _receiverService.GetReceiversForListAsync(pageSize, pageNo, searchString, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

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
            var serviceResponse = await _receiverService.UpdateReceiverAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReceiver(int receiverId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.GetReceiverForDeleteAsync(receiverId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReceiver(ReceiverForDeleteVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.DeleteReceiverByIdAsync(model.Id, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            TempData["SuccessMessage"] = $"Receiver with ID {model.Id} has been successfully deleted.";
            return RedirectToAction("ViewReceiverList");
        }

        [HttpGet]
        public async Task<IActionResult> AddReceiverBranch(CancellationToken cancellationToken, int receiverId = 0)
        {
            var model = await _receiverService.PrepareNewReceiverBranchVm(cancellationToken, receiverId);

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
        public async Task<IActionResult> UpdateReceiverBranch(int receiverBranchId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReceiverBranch(UpdateReceiverBranch model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        [HttpGet]
        public async Task<IActionResult> AddContactForReceiver(int receiverId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.PrepareAddContactVm(receiverId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddContactForReceiver(NewContactVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _receiverService.AddReceiverContactAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return RedirectToAction("ViewContact", "Contact", new { contactId = serviceResponse.ObjectId });
        }
    }
}
