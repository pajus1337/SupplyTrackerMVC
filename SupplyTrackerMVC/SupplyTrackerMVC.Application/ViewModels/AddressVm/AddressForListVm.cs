using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.AddressVm
{
    public class AddressForListVm
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
    }
}
