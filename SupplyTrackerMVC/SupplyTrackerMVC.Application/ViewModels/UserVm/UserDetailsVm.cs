using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyTrackerMVC.Application.ViewModels.UserVm
{
    public class UserDetailsVm : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
       // public string FirstName { get; set; }
       // public string LastName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public bool IsActive { get; set; }
        // public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsVm>();
        }
    }
}
