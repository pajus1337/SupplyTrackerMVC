using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.Common;

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
            return View(new AddContactDetailTypeVm());
        }

        [HttpPost]
        public async Task<IActionResult> AddContactType(AddContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _contactService.AddContactDetailTypeAsync(model, cancellationToken);

            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> EditContactType(int contactTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> EditContactType(UpdateContactDetailTypeVm model, CancellationToken cancellationToke)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteContactType(int ContactTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContactType(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet] 
        public async Task<IActionResult> ViewContactTypeDetails(int contactTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
