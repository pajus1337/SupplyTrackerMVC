using SupplyTrackerMVC.Domain.Interfaces.Common;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Receivers
{
    public class Receiver : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? LogoPic { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        // 1:1 
        public int AddressId { get; set; }
        public Address Address { get; set; }

        // 1:N
        public virtual ICollection<ReceiverBranch> DeliveryBranchs { get; set; }

        // 1:N
        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
