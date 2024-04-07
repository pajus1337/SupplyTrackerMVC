
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
        public string Name { get; set; }

        public ICollection<ContactDetail> ContactDetails { get; set; }
    }
}
