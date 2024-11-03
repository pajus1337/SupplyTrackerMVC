using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.Common;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class AdminController(IAdminService adminService) : BaseController
    {
        private readonly IAdminService _adminService = adminService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewContactTypesList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _adminService.GetContactDetailTypeForListAsync(cancellationToken);

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
            var serviceResponse = await _adminService.AddContactDetailTypeAsync(model, cancellationToken);

            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> EditContactType(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> EditContactType(UpdateContactDetailTypeVm model, CancellationToken cancellationToke)
        {
            throw new NotImplementedException();
        }
             

    }
}
