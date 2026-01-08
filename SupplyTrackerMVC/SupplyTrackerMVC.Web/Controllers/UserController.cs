using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.UserVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class UserController : BaseController
    {

        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ViewUserList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _userService.GetUsersForListAsync(5, 1, string.Empty, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }


        [HttpPost]
        public async Task<IActionResult> ViewUserList(int pageSize, CancellationToken cancellationToken, int pageNo = 1, string searchString = "")
        {
            var serviceResponse = await _userService.GetUsersForListAsync(pageSize, pageNo, searchString, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ViewUser(string userId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _userService.GetUserDetailsByIdAsync(userId, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string userId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _userService.GetUserForDeleteAsync(userId.ToString(), cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(UserForDeleteVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _userService.DeleteUserByIdAsync(model.Id, cancellationToken);
            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            TempData["SuccessMessage"] = $"User with ID {model.Id} has been successfully deleted.";
            return RedirectToAction("ViewUserList");
        }
    }
}
