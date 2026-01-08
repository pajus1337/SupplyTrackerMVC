using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyTrackerMVC.Domain.Model.User
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
    }
}
