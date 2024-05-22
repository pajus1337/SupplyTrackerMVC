using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.ModelBuilders
{
    public class ProductDetailModelBuilder : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.HasQueryFilter(pd => !pd.Product.IsDeleted);

            builder
                .HasOne(pd => pd.Product)
                .WithOne(p => p.ProductDetail)
                .HasForeignKey<ProductDetail>(pd => pd.ProductRef)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
