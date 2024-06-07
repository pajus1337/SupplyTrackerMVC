using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class NewReceiverBranchVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BranchInternalID { get; set; }
        public string BranchAlias { get; set; }

        public int AddressId { get; set; }
        public NewAddressForReceiverBranchVm NewAddressForReceiverBranch { get; set; }

        public int ContactId { get; set; }
        public NewContactVm NewContactForReceiverBranch { get; set; }
        public int ReceiverSelectedId { get; set; }
        public ReceiverSelectListVm ReceiverSelectList { get; set; }
    }
}
