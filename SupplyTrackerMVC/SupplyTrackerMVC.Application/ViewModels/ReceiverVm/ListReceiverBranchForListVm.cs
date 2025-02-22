using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ListReceiverBranchForListVm
    {
        public List<ReceiverBranchForListVm> ReceiverBranches { get; set; }
        public int Count { get; set; }
    }
}
