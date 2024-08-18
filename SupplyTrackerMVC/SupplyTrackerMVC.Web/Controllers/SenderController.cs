using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    [Route("Sender")]
    public class SenderController : Controller
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
            return View();
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
                if (serviceResponse.ErrorMessage != null)
                {
                    foreach (var error in serviceResponse.ErrorMessage)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }

                return View(model);
            }

            return RedirectToAction("ViewSender", new { senderId = serviceResponse.ObjectId });
        }

        [HttpGet]
        [Route("sender-details")]
        public async Task<IActionResult> ViewSender(int senderId,CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.GetSenderDetailsByIdAsync(senderId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return NotFound();
            }
            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> EditSender(int senderId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _senderService.GetSenderDetailsByIdAsync(senderId, cancellationToken);
            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> EditSender(CancellationToken cancellationToken)
        {
           
            return View();
        }

        [HttpGet]
        [Route("list-of-senders")]
        public async Task<ActionResult> ListOfSenders(CancellationToken cancellationToken)
        {
            var serviceResponse =  await _senderService.GetSendersForListAsync(cancellationToken);

            return View(serviceResponse.Data);
        }
    }
}
