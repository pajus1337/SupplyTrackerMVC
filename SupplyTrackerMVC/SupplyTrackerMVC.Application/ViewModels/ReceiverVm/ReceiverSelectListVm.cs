using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverSelectListVm
    {
        public IEnumerable<ReceiverForSelectListVm> Receivers { get; set; }
    }
}
