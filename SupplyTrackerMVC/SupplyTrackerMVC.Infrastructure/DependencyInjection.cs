using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Domain.Interfaces;
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
            serviceDescriptors.AddTransient<IReceiverRepository, ReceiverRepository>();

            return serviceDescriptors;
        }
    }
}
