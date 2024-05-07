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

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("new-sender")]
        [HttpGet]
        public IActionResult AddSender()
        {
            return View(new NewSenderVm());
        }

        [Route("new-sender")]
        [HttpPost]
        public async Task<IActionResult> AddSender(NewSenderVm model, CancellationToken cancellationToken)
        {
            var (success, errors, senderId) = await _senderService.AddNewSenderAsync(model, cancellationToken);
            if (!success)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(model);
            }

            return RedirectToAction("ViewSender", new { senderId = senderId });
        }

        [Route("sender-details")]
        [HttpGet]
        public async Task<IActionResult> ViewSender(int senderId,CancellationToken cancellationToken)
        {
            var model = await _senderService.GetSenderDetailsByIdAsync(senderId, cancellationToken);
            return View(model);
        }

        [Route("list-of-senders")]
        [HttpGet] 
        public IActionResult ListOfSenders()
        {
            var model = _senderService.GetAllActiveSendersForList();

            return View(model);
        }
    }
}
