using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System.Threading;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ContactController(IContactService contactService) : BaseController
    {
        private readonly IContactService _contactService = contactService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewContactTypesList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.GetContactDetailTypesForListAsync(cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddContactType(CancellationToken cancellationToken)
        {
            return View(new NewContactDetailTypeVm());
        }

        [HttpPost]
        public async Task<IActionResult> AddContactType(NewContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.AddContactDetailTypeAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            ViewBag.ReturnUrl = Url.Action("ViewContactTypesList");
            ViewBag.Message = $"Contact type with ID {serviceResponse.ObjectId} has been successfully created";

            return View("GenericConfirmation");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContactType(int contactTypeId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.GetContactDetailTypeForEditAsync(contactTypeId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactType(UpdateContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.UpdateContactDetailTypeAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse, model);
            }

            return RedirectToAction("ViewContactTypeDetails", new { contactTypeId = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteContactType(int ContactTypeId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.GetContactDetailTypeForDeleteAsync(ContactTypeId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContactType(ContactDetailTypeForDeleteVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.DeleteContactDetailTypeAsync(model.Id, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            ViewBag.ReturnUrl = Url.Action("ViewContactTypesList");
            ViewBag.Message = $"Contact type with ID {model.Id} has been successfully deleted";

            return View("GenericConfirmation");
        }

        [HttpGet]
        public async Task<IActionResult> ViewContactTypeDetails(int contactTypeId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.GetContactDetailTypeAsync(contactTypeId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        // TODO: Create Prototype, test and refine at end this function.
        [HttpGet]
        public async Task<IActionResult> ViewContact(int contactId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.GetContactAsync(contactId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int contactId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.GetContactForUpdateAsync(contactId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.UpdateContactAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContactDetail(int contactDetailId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.GetContactDetailForUpdateAsync(contactDetailId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactDetail(UpdateContactDetailVm model, CancellationToken cancellationToke)
        {
            var serviceResponse = await _contactService.UpdateContactDetailAsync(model, cancellationToke);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            ViewBag.ReturnUrl = Url.Action("UpdateContactDetail", new { contactDetailId = model.Id });
            ViewBag.Message = $"Contact detail with ID {model.Id} has been successfully updated";

            return View("GenericConfirmation");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContactDetail(int contactDetailId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.DeleteContactDetailAsync(contactDetailId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            ViewBag.Message = $"Contact detail with ID {contactDetailId} has been successfully deleted";

            return View("GenericConfirmation");
        }

        [HttpGet]
        public async Task<IActionResult> AddContactDetail(int contactId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.PrepareAddContactDetailVmAsync(contactId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            var model = serviceResponse.Data;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddContactDetail(NewContactDetailVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.AddContactDetailAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            ViewBag.ReturnUrl = Url.Action("ViewContact", new { contactId = model.ContactId });
            ViewBag.Message = $"Contact detail with ID {serviceResponse.ObjectId} has been successfully created";

            return View("GenericConfirmation");
        }
    }
}