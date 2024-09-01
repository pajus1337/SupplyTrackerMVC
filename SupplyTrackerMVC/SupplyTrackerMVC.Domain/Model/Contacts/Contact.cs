using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Contacts
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public ICollection<ContactDetail> ContactDetails { get; set; }
        public int? ReceiverId { get; set; }
        public Receiver? Receiver { get; set; }
        public int? SenderId { get; set; }
        public Sender? Sender { get; set; }
    }
}