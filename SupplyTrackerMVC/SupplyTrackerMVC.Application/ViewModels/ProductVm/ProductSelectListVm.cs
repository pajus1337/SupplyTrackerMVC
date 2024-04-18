using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ProductVm
{
    public class ProductSelectListVm
    {
        public IEnumerable<ProductForSelectListVm> Products { get; set; }
    }
}
