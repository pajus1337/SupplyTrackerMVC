using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverBranchForSelectListVm : IMapFrom<ReceiverBranch>
    {
        public int Id { get; set; }
        public string BranchAlias { get; set; }
        public string BranchInternalID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReceiverBranch, ReceiverBranchForSelectListVm>();
        }
    }
}
