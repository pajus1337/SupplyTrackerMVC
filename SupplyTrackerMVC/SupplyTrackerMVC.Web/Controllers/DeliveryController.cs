﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class DeliveryController : BaseController
    {
        private readonly IDeliveryService _deliveryService;
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewDelivery()
        {
            var model = _deliveryService.PrepareNewDeliveryViewModel();
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> NewDelivery(NewDeliveryVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _deliveryService.AddNewDeliveryAsync(model, cancellationToken);

            if (!serviceResponse.Success)
            {
                if (serviceResponse.ErrorMessage != null)
                {
                    foreach (var error in serviceResponse.ErrorMessage)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                return View("NewDelivery", model);
            }

            // TODO: Create DeliveryDetails Metod.
            return RedirectToAction("DeliveryDetails", new { deliveryId = serviceResponse.ObjectId });
        }

        [HttpGet]
        public async Task<IActionResult> DeliveryDetails(int deliveryId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _deliveryService.GetDeliveryDetailsByIdAsync(deliveryId, cancellationToken);

            //TODO: Add handling for negative serviceResponse

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public IActionResult GetReceiverBranches(int receiverId)
        {
            var branches = _deliveryService.GetReceiverBranchesByReceiverId(receiverId);
            return Json(branches);
        }


        // TODO: Create Pagination and Add more search filters ( Find firs out how to, ( best way )) ;)) 
        [HttpGet]
        public async Task<IActionResult> GetDeliveriesList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _deliveryService.GetDeliveryForListAsync(cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }
    }
}
