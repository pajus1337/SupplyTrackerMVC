using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model
{
    public class Receiver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReceiverInternalID { get; set; }
    }
}
