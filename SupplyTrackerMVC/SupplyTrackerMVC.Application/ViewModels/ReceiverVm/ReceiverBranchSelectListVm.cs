using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverBranchSelectListVm
    {
        public IEnumerable<ReceiverBranchForSelectListVm> ReceiverBranches { get; set; }
    }
}
