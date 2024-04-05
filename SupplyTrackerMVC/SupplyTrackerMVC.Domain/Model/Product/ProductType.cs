using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Product
{
    public class ProductType
    {
        public int Id { get; set; }
        public string PhysicalState { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
