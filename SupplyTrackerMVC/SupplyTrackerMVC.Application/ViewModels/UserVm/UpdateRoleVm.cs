using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyTrackerMVC.Application.ViewModels.UserVm
{
    internal class UpdateRoleVm
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> AvailableRoles { get; set; }
        public string RoleName { get; set; }
    }
}
