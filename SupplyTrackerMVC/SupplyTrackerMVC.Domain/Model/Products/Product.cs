using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Products
{
    public class Product : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        // TODO : Remove isActive since we implementing SoftDelet via EF Interceptor
        public bool isActive { get; set; }
        // N:1
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        // 1:1
        public ProductDetail ProductDetail { get; set; }
    }
}
