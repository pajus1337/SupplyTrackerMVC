using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverBranchForSelectListVm
    {
        public int Id { get; set; }
        public string BranchAlias { get; set; }
        public string BranchInternalID { get; set; }
    }
}
