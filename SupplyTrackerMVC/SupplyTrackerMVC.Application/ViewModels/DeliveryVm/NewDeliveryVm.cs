using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.DeliveryVm
{
    public class NewDeliveryVm
    {
        public int Id { get; set; }
        public DateTime DeliveryDataTime { get; set; }
        public int SelectedReceiverId { get; set; }
        public IEnumerable<Receiver> Receivers { get; set; }
        public int ProductDeliveryWeight { get; set; }
    }
}
