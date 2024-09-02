using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Contacts
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public string ContactDetailValue { get; set; }
        public int ContactDetailTypeId { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
