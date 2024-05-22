using SupplyTrackerMVC.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Model.Products
{
    public class ProductDetail : ISoftDeletable
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
        public string ChemicalSymbol { get; set; }
        public string ChemicalName { get; set; }
        public string ProductDescription { get; set; }
        public int MassFraction { get; set; }

        public int ProductRef { get; set; }
        public Product Product { get; set; }
    }
}
