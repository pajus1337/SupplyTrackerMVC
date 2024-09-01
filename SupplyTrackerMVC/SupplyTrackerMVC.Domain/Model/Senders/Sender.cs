using SupplyTrackerMVC.Domain.Interfaces.Common;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Senders
{
    public class Sender : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? LogoPic { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
