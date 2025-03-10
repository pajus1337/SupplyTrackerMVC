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
    // TODO: Create and Add Validation Rules, Add more properties, importenet in edition of this object. Adjust Mapping with automapper,
    public class UpdateReceiverBranch : IMapFrom<ReceiverBranch>
    {
        public int Id { get; set; }
        [DisplayName("Name of this Branch")]
        public string Name { get; set; }
        [DisplayName("Internal ID of this branch")]
        public string BranchInternalID { get; set; }
        [DisplayName("Alias name of this branch")]
        public string BranchAlias { get; set; }

        public void Mapping(Profile profile)
        {

        }
    }
}