﻿using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class NewReceiverBranchVm : IMapFrom<ReceiverBranch>
    {
        public int Id { get; set; }
        [DisplayName("Name for new branch")]
        public string Name { get; set; }
        [DisplayName("Internal ID using for this branch")]
        public string BranchInternalID { get; set; }
        [DisplayName("Alias name of this branch")]
        public string BranchAlias { get; set; }

        public int AddressId { get; set; }
        public NewAddressForReceiverBranchVm NewAddressForReceiverBranch { get; set; }

        public int ContactId { get; set; }
        public AddContactVm NewContactForReceiverBranch { get; set; }
        [DisplayName("Select Receiver you create new branch for")]
        public int ReceiverSelectedId { get; set; }
        public ReceiverSelectListVm ReceiverSelectList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewReceiverBranchVm, ReceiverBranch>()
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.NewAddressForReceiverBranch))
                .ForMember(d => d.ReceiverId, opt => opt.MapFrom(s => s.ReceiverSelectedId));
        }
    }
}
