using SupplyTrackerMVC.Domain.Interfaces.Common;
using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Deliveries
{
    public class Delivery : ISoftDeletable
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
        public DateTime DeliveryDataTime { get; set; }

        // 1:N
        public int SenderId { get; set; }
        public Sender Sender { get; set; }

        // 1:N 
        public int ReceiverId { get; set; }
        public Receiver Receiver { get; set; }

        public int ReceiverBranchId { get; set; }              
        public ReceiverBranch ReceiverBranch { get; set; }

        // 1:N 
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductDeliveryWeight { get; set; }
    }
}
