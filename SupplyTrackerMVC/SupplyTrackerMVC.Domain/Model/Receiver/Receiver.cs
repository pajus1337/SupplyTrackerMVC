using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Receiver
{
    public class Receiver
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 1:N
        public ICollection<DeliveryBranch> DeliveryBranchs { get; set; }
    }
}
