using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.UserVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IUserService
    {
        Task<ActionResponse<VoidValue>> UpdateUserRoleAsync(string userId, string roleName, CancellationToken cancellationToken);
        Task<ActionResponse<ListUserForListVm>> GetUsersForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<ActionResponse<UserDetailsVm>> GetUserDetailsByIdAsync(string userId, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> DeleteUserByIdAsync(string userId, CancellationToken cancellationToken);
        Task<ActionResponse<UserForDeleteVm>> GetUserForDeleteAsync(string userId, CancellationToken cancellationToken);
    }
}
