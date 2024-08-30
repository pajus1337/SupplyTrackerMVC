using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class ListContactDetailTypesForListVm
    {
        public List<ContactDetailTypeForList> ContactDetailTypes { get; set; }
        public int Count { get; set; }
    }
}
