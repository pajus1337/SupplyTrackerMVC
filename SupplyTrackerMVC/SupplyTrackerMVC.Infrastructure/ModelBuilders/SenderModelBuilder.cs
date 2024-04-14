using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.ModelBuilders
{
    public class SenderModelBuilder : IEntityTypeConfiguration<Sender>
    {
        public void Configure(EntityTypeBuilder<Sender> builder)
        {
            builder
                .HasOne(r => r.Address)
                .WithOne()
                .HasForeignKey<Sender>(r => r.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
