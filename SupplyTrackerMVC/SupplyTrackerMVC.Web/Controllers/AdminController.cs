using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
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
            // TODO: Refine method => since this belowe is just a prototype.
            var model = await _adminService.GetListContactDetailTypeAsync(cancellationToken);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddContactType(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddContactType(AddContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
