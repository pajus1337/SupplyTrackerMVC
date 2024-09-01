using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class ListContactDetailTypesForListVm
    {
        public List<ContactDetailTypeForListVm> ContactDetailTypes { get; set; }
        public int Count { get; set; }
    }
}
