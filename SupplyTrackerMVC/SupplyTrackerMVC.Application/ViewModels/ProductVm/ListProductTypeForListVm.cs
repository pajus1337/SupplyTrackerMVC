using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ProductVm
{
    public class ListProductTypeForListVm
    {
        public List<ProductTypeForListVm> ProductTypes { get; set; }
        public int Count { get; set; }
    }
}
