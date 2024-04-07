using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Receivers
{
    public class DeliveryBranch
    {
        public int Id { get; set; }
        public string BranchInternalID { get; set; }

        public int ReceiverId { get; set; }
        public Receiver Receiver { get; set; }
    }
}
