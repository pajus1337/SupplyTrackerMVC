using FluentValidation;
using SupplyTrackerMVC.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using static SupplyTrackerMVC.Application.ViewModels.ReceiverVm.NewReceiverVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ReceiverController : Controller
    {
        private readonly IReceiverService _receiverService;
        private readonly IValidator<NewReceiverVm> _validator;

        public ReceiverController(IReceiverService receiverService, IValidator<NewReceiverVm> validator )
        {
            _validator = validator;
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
        public async Task<IActionResult> AddReceiver(NewReceiverVm model)
        {
            var (success, errors, receiverId) = await _receiverService.AddNewReceiverAsync(model);
            if (!success)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View("AddReceiver", model);
            }

            return RedirectToAction("ViewReceiver", new { receiverId = receiverId });
        }

        public IActionResult ViewReceiver(int receiverId)
        {
            return View();
        }
    }
}
