using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public int ProductId { get; set; }
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string message)
            : base(message)
        {
        }

        public ProductNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ProductNotFoundException(string message, int productId)
    : base(message)
        {
            this.ProductId = productId;
        }
    }
}

