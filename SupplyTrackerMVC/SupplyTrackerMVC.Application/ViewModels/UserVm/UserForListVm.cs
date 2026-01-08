using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Senders;
using SupplyTrackerMVC.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplyTrackerMVC.Application.ViewModels.UserVm
{
    public class UserForListVm : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserForListVm>();
        }
    }
}
