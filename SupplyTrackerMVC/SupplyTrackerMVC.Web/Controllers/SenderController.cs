using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SupplyTrackerMVC.Application.Interfaces;
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

            if (!serviceResponse.Success)
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
            if (!serviceResponse.Success && serviceResponse.ErrorMessage != null)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        [Route("edit-sender")]
        public async Task<IActionResult> EditSender(int senderId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.GetSenderForEditAsync(senderId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpPost]
        [Route("edit-sender")]
        public async Task<IActionResult> EditSender(UpdateSenderVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.UpdateSenderByIdAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse, model);
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        [Route("list-of-senders")]
        public async Task<IActionResult> ListOfSenders(CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.GetSendersForListAsync(cancellationToken);
            if (!serviceResponse.Success)
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
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpPost]
        [Route("delete-sender")]
        public async Task<IActionResult> DeleteSender(SenderForDeleteVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.DeleteSenderAsync(model.Id, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }
            // TODO: View Confirmation ? 
            return View(serviceResponse.AdditionalMessage);
        }

        // TODO: AddContact, DeleteContact, EditContact - For Sender.
    }
}
