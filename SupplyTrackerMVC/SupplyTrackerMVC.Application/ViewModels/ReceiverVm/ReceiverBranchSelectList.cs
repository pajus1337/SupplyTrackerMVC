using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverBranchSelectList
    {
        public IQueryable<ReceiverBranchForSelectListVm> ReceiverBranches { get; set; }
    }
}
