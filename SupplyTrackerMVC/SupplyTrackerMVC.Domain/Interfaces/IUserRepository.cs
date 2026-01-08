using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetAllUsers();
        IQueryable<User> GetUserById(string userId);
        Task<IList<string>> GetUserRolesAsync(string userId, CancellationToken cancellationToken);
        Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken);
        Task<bool> UpdateUserRoleAsync(string userId, string roleName, CancellationToken cancellationToken);
    }
}
