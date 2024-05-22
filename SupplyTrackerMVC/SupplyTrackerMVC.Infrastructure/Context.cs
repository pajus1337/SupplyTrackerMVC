using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using SupplyTrackerMVC.Infrastructure.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<ContactDetailType> ContactDetailTypes { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<ReceiverBranch> DeliveryBranches { get; set; }
        public DbSet<Sender> Senders { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AddressModelBuilder());
            // modelBuilder.ApplyConfiguration(new ContactModelBuilder());
            modelBuilder.ApplyConfiguration(new DeliveryModelBuilder());
            modelBuilder.ApplyConfiguration(new ProductModelBuilder());
            modelBuilder.ApplyConfiguration(new ProductDetailModelBuilder());
            modelBuilder.ApplyConfiguration(new ReceiverBranchModelBuilder());
            modelBuilder.ApplyConfiguration(new ReceiverModelBuilder());
            modelBuilder.ApplyConfiguration(new SenderModelBuilder());
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
