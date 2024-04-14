using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddTransient<IReceiverService, ReceiverService>();
            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }
    }
}
