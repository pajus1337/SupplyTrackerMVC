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

            // Address - 1:1
            builder
                .HasOne(rb => rb.Address)
                .WithOne()
                .HasForeignKey<ReceiverBranch>(rb => rb.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Receiver - N:1
            builder
                .HasOne(rb => rb.Receiver)
                .WithMany(r => r.ReceiverBranches)
                .HasForeignKey(rb => rb.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Deliveries - 1:N
            builder
                .HasMany(rb => rb.Deliveries)
                .WithOne(d => d.ReceiverBranch)
                .HasForeignKey(d => d.ReceiverBranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
