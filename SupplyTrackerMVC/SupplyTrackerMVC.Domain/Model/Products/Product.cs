using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // N:1
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        // 1:1
        public ProductDetail ProductDetail { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
    }
}
