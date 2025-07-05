﻿using SupplyTrackerMVC.Domain.Interfaces.Common;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Receivers
{
    public class ReceiverBranch : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BranchInternalID { get; set; }
        public string BranchAlias { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int ReceiverId { get; set; }
        public Receiver Receiver { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
    }
}
