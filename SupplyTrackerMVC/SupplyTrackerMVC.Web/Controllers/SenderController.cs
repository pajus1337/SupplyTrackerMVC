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

        [HttpGet]
        [Route("sender-details")]
        public async Task<IActionResult> ViewSender(int senderId,CancellationToken cancellationToken)
        {
            var (success, model) = await _senderService.GetSenderDetailsByIdAsync(senderId, cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpGet]
        [Route("list-of-senders")]
        public IActionResult ListOfSenders(CancellationToken cancellationToken)
        {
            var model = _senderService.GetAllSendersForListAsync(cancellationToken);

            return View(model);
        }
    }
}
