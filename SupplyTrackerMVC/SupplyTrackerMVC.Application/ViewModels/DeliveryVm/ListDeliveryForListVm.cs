using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.DeliveryVm
{
    public class ListDeliveryForListVm
    {
        public List<DeliveryForListVm>  Deliveries { get; set; }
        public int Count { get; set; }
    }
}
