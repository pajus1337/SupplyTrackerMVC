
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.ModelBuilders
{
    public class ReceiverModelBuilder : IEntityTypeConfiguration<Receiver>
    {
        public void Configure(EntityTypeBuilder<Receiver> builder)
        {
            builder.HasQueryFilter(r => !r.IsDeleted);

            builder
                .HasIndex(r => r.IsDeleted)
                .HasFilter("IsDeleted = 0");

            builder
                .HasOne(r => r.Address)
                .WithOne()
                .HasForeignKey<Receiver>(r => r.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
