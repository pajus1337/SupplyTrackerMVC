using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.ModelBuilders
{
    public class ReceiverBranchModelBuilder : IEntityTypeConfiguration<ReceiverBranch>
    {
        public void Configure(EntityTypeBuilder<ReceiverBranch> builder)
        {
            builder.HasQueryFilter(rb => !rb.IsDeleted);

            builder
                .HasOne(rb => rb.Address)
                .WithOne()
                .HasForeignKey<ReceiverBranch>(rb => rb.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
