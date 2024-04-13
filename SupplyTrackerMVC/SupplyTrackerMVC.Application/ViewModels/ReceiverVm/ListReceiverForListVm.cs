using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ListReceiverForListVm
    {
        public List<ReceiverForListVm> Receivers { get; set; }
        public int Count { get; set; }
    }
}
