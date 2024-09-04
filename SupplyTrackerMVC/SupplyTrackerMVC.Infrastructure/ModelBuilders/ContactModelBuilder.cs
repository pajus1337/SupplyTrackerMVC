using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.ModelBuilders
{
    public class ContactModelBuilder : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasMany(c => c.ContactDetails)
                   .WithOne(cd => cd.Contact)
                   .HasForeignKey(cd => cd.ContactId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Sender)
                   .WithMany(s => s.Contacts)
                   .HasForeignKey(c => c.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Receiver)
                   .WithMany(r => r.Contacts)
                   .HasForeignKey(c => c.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
