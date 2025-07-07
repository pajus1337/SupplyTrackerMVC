using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    [Route("Sender")]
    public class SenderController : BaseController
    {
        private readonly ISenderService _senderService;

        public SenderController(ISenderService senderService)
        {
            _senderService = senderService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return NotFound(new NotFoundResult());
        }

        [HttpGet]
        [Route("new-sender")]
        public IActionResult AddSender()
        {
            return View(new NewSenderVm());
        }

        [HttpPost]
        [Route("new-sender")]
        public async Task<IActionResult> AddSender(NewSenderVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.AddNewSenderAsync(model, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse, model);
            }

            return RedirectToAction("ViewSender", new { senderId = serviceResponse.ObjectId });
        }

        [HttpGet]
        [Route("sender-details")]
        public async Task<IActionResult> ViewSender(int senderId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.GetSenderDetailsByIdAsync(senderId, cancellationToken);
            if (!serviceResponse.IsSuccessful && serviceResponse.ErrorMessage != null)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        [Route("edit-sender")]
        public async Task<IActionResult> UpdateSender(int senderId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.GetSenderForEditAsync(senderId, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpPost]
        [Route("edit-sender")]
        public async Task<IActionResult> UpdateSender(UpdateSenderVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.UpdateSenderAsync(model, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse, model);
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        [Route("list-of-senders")]
        public async Task<IActionResult> ViewSenderList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.GetSendersForListAsync(5, 1, "", cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpPost]
        [Route("list-of-senders")]
        public async Task<IActionResult> ViewSenderList(int pageSize, CancellationToken cancellationToken, int pageNo = 1, string searchString = "")
        {
            var serviceResponse = await _senderService.GetSendersForListAsync(pageSize, pageNo, searchString, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        [Route("delete-sender")]
        public async Task<IActionResult> DeleteSender(int senderId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.GetSenderForDeleteAsync(senderId, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpPost]
        [Route("delete-sender")]
        public async Task<IActionResult> DeleteSender(SenderForDeleteVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.DeleteSenderByIdAsync(model.Id, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            TempData["SuccessMessage"] = $"Sender with ID {model.Id} has been successfully deleted.";
            return RedirectToAction("ViewSenderList");
        }


        // TODO: Refine this AddContactForSender prototype method's
        [HttpGet]
        [Route("create-new-contact")]
        public async Task<IActionResult> AddContactForSender(int senderId)
        {
            var serviceResponse = await _senderService.PrepareAddContactVm(senderId);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        [Route("create-new-contact")]
        public async Task<IActionResult> AddContactForSender(NewContactVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.AddSenderContactAsync(model, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            return RedirectToAction("ViewContact", "Contact", new { contactId = serviceResponse.ObjectId });
        }
        // TODO: AddContact, DeleteContact, EditContact - For Sender.

        [HttpGet]
        [Route("view-sender-contact")]
        public async Task<IActionResult> ViewSenderContact(int senderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
