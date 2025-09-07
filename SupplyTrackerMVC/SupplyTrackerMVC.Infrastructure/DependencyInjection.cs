using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Infrastructure.ExternalServices.Email;
using SupplyTrackerMVC.Infrastructure.ExternalServices.Reporting;
using SupplyTrackerMVC.Infrastructure.Interceptors;
using SupplyTrackerMVC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors)
        {

            // Repositories 
            serviceDescriptors.AddTransient<IAddressRepository, AddressRepository>();
            serviceDescriptors.AddTransient<IContactRepository, ContactRepository>();
            serviceDescriptors.AddTransient<IDeliveryRepository, DeliveryRepository>();
            serviceDescriptors.AddTransient<IProductRepository, ProductRepository>();
            serviceDescriptors.AddTransient<IReceiverRepository, ReceiverRepository>();
            serviceDescriptors.AddTransient<ISenderRepository, SenderRepository>();

            // Generators
            serviceDescriptors.AddTransient<IReportGenerator, ReportGenerator>();

            // Interceptors
            serviceDescriptors.AddSingleton<SoftDeleteInterceptor>();

            // E-mail (sendmail)
            serviceDescriptors.AddTransient<IEmailSender, SendmailEmailSender>();

            return serviceDescriptors;
        }
    }
}
