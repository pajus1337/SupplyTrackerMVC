using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ReceiverController : Controller
    {
        public IActionResult Index()
        {
            var model = IReceiverService.GetAllReceiversForList();
            return View();
        }

        [HttpGet]
        public IActionResult AddReceiver()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddReceiver(ReceiverModel receiverModel)
        {
            return View();
        }

        public IActionResult ViewReceiver(int receiverId)
        {
            return View();
        }
    }
}
