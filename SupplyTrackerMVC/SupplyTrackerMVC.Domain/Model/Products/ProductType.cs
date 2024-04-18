using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Products
{
    public class ProductType
    {
        public int Id { get; set; }
        public string PhysicalState { get; set; }
        public bool IsADRProduct { get; set; }

        // 1:N
        public ICollection<Product> Products { get; set; }
    }
}
