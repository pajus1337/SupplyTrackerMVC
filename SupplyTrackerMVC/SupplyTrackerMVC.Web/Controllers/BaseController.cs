using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Responses;

namespace SupplyTrackerMVC.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult HandleErrors<T>(ActionResponse<T> serviceResponse, object model = null)
        {
            if (serviceResponse.IsValidationError)
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
            else
            {
                ViewBag.ErrorMessage = string.Join(", ", serviceResponse.ErrorMessage);
                return View("ResponseError");
            }
        }
    }
}
