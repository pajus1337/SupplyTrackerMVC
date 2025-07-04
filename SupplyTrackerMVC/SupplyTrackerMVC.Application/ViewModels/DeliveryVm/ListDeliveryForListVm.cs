using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.DeliveryVm
{
    public class ListDeliveryForListVm
    {
        public List<DeliveryForListVm> Deliveries { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchBy { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
