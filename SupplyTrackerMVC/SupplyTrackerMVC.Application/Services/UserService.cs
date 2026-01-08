using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.UserVm;
using SupplyTrackerMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyTrackerMVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ActionResponse<VoidValue>> UpdateUserRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return ActionResponse<VoidValue>.Failed(errorMessage: new[] { "Invalid user ID" });
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                return ActionResponse<VoidValue>.Failed(errorMessage: new[] { "Role name cannot be empty." }, isValidationError: true);
            }

            try
            {
                var userQuery = _userRepository.GetUserById(userId.ToString());
                var user = await userQuery.SingleOrDefaultAsync();

                if (user == null)
                {
                    return ActionResponse<VoidValue>.Failed(errorMessage: new[] { "User not found in Db" });
                }

                user.RoleName = roleName.Trim();
                var success = await _userRepository.UpdateUserRoleAsync(user.Id,user.RoleName, CancellationToken.None);

                if (!success)
                {
                    return ActionResponse<VoidValue>.Failed(errorMessage: new[] { "Could not update user role." });
                }

                return ActionResponse<VoidValue>.Success(new VoidValue(), additionalMessage: "User role updated successfully.");
            }
            catch (Exception ex)
            {
                return ActionResponse<VoidValue>.Failed(errorMessage: new[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<VoidValue>> DeleteUserByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return ActionResponse<VoidValue>.Failed(errorMessage: new[] { "Invalid user ID" });
            }

            try
            {
                var success = await _userRepository.DeleteUserAsync(userId, cancellationToken);

                if (!success)
                {
                    return ActionResponse<VoidValue>.Failed(errorMessage: new[] { "Failed to delete user" });
                }

                return ActionResponse<VoidValue>.Success(new VoidValue());
            }
            catch (Exception ex)
            {
                return ActionResponse<VoidValue>.Failed(errorMessage: new[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<UserForDeleteVm>> GetUserForDeleteAsync(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return ActionResponse<UserForDeleteVm>.Failed(errorMessage: new[] { "Invalid user ID" });
            }

            try
            {
                var userQuery = _userRepository.GetUserById(userId).ProjectTo<UserForDeleteVm>(_mapper.ConfigurationProvider);

                var userVm = await userQuery.SingleOrDefaultAsync(cancellationToken);

                if (userVm == null)
                {
                    return ActionResponse<UserForDeleteVm>.Failed(errorMessage: new[] { "User not found in Db" });
                }

                return ActionResponse<UserForDeleteVm>.Success(userVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<UserForDeleteVm>.Failed(errorMessage: new[] { $"Error occurred -> {ex.Message}" });
            }
        }


        public async Task<ActionResponse<UserDetailsVm>> GetUserDetailsByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return ActionResponse<UserDetailsVm>.Failed(errorMessage: new[] { "Invalid user ID" });
            }

            try
            {
                var userQuery = _userRepository.GetUserById(userId).ProjectTo<UserDetailsVm>(_mapper.ConfigurationProvider);

                var userVm = await userQuery.SingleOrDefaultAsync(cancellationToken);
                if (userVm == null)
                {
                    return ActionResponse<UserDetailsVm>.Failed(errorMessage: new[] { "User not found in Db" });
                }

                return ActionResponse<UserDetailsVm>.Success(userVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<UserDetailsVm>.Failed(errorMessage: new[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ListUserForListVm>> GetUsersForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            try
            {
                var usersQuery = _userRepository.GetAllUsers();

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    var term = searchString.Trim();
                    usersQuery = usersQuery.Where(u =>
                        u.UserName.StartsWith(term) ||
                        u.Email.StartsWith(term));
                }

                usersQuery = usersQuery.OrderBy(u => u.Id);

                var usersToShow = usersQuery
                    .Skip(pageSize * (pageNo - 1))
                    .Take(pageSize);

                var users = await usersToShow.ToListAsync(cancellationToken);

                var result = new ListUserForListVm
                {
                    Users = users
                        .Select(user => _mapper.Map<UserForListVm>(user))
                        .ToList(),
                    CurrentPage = pageNo,
                    PageSize = pageSize,
                    SearchString = searchString,
                    Count = await usersQuery.CountAsync(cancellationToken)
                };

                return ActionResponse<ListUserForListVm>.Success(data: result);
            }
            catch (Exception ex)
            {
                return ActionResponse<ListUserForListVm>.Failed(errorMessage: new[] { $"Error occurred -> {ex.Message}" });
            }
        }
    }
}

