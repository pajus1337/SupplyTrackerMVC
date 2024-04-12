﻿using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Deliveries
{
    public class Delivery
    {
        public int Id { get; set; }
        public DateTime DeliveryDataTime { get; set; }

        // 1:N
        public int SenderId { get; set; }
        public Sender Sender { get; set; }

        // 1:N 
        public int ReceiverId { get; set; }
        public Receiver receiver { get; set; }

        // 1:N 
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
