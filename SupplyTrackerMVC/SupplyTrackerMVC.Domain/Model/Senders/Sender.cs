using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Senders
{
    public class Sender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte LogoPic { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
    }
}
