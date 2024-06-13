using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverBranchDetailsVm : IMapFrom<ReceiverBranch>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BranchInternalID { get; set; }
        public string BranchAlias { get; set; }

        public int AddressId { get; set; }
        public AddressDetailsVm AddressDetails { get; set; }

        public int ContactId { get; set; }
        public ContactDetailsVm ContactDetails { get; set; }
        public int ReceiverId { get; set; }
        public ReceiverDetailsForRbVm ReceiverDetails { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReceiverBranch, ReceiverBranchDetailsVm>();
        }
    }
}
