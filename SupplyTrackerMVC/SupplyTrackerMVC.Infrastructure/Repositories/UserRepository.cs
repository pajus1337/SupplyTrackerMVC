using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _userManager.Users
                .AsNoTracking()
                .Select(u => new User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActive = !u.LockoutEnd.HasValue || u.LockoutEnd <= DateTimeOffset.Now
                });
        }

        public IQueryable<User> GetUserById(string userId)
        {

            return _userManager.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActive = !u.LockoutEnd.HasValue || u.LockoutEnd <= DateTimeOffset.Now
                });
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new List<string>();
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<bool> DeleteUserAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return false;
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return false;
            }

            var addResult = await _userManager.AddToRoleAsync(user, roleName);
            return addResult.Succeeded;
        }
    }
}
