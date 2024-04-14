using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<ContactDetailType> ContactDetailTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ReceiverBranch> DeliveryBranches { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Sender> Senders { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductDetail)
                .WithOne(pd => pd.Product)
                .HasForeignKey<ProductDetail>(pd => pd.ProductRef);

            modelBuilder.Entity<Receiver>()
                .HasOne(r => r.Address)
                .WithOne()
                .HasForeignKey<Receiver>(r => r.AddressId)
                .OnDelete(DeleteBehavior.Restrict) ;

            modelBuilder.Entity<ReceiverBranch>()
                .HasOne(rb => rb.Address)
                .WithOne()
                .HasForeignKey<ReceiverBranch>(rb => rb.AddressId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Sender)
                .WithMany(s => s.Deliveries)
                .HasForeignKey(d => d.SenderId);


            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Receiver)
                .WithMany(r => r.Deliveries)
                .HasForeignKey(d => d.ReceiverId);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Product)
                .WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.ProductID);
        }
    }
}
