using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.ModelBuilders
{
    public class DeliveryModelBuilder : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.HasQueryFilter(d => !d.IsDeleted);

            builder
                .HasOne(d => d.Sender)
                .WithMany(s => s.Deliveries)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(d => d.Receiver)
                .WithMany(r => r.Deliveries)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
