using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class SenderController : Controller
    {
        private readonly ISenderService _senderService;

        public SenderController(ISenderService senderService)
        {
            _senderService = senderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSender()
        {
            return View(new NewSenderVm());
        }

        [HttpPost]
        public IActionResult AddSender(NewSenderVm model)
        {
            return View();
        }
    }
}
