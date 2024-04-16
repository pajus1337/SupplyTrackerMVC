using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SupplyTrackerMVC.Application.ViewModels.ReceiverVm.NewReceiverVm;

namespace SupplyTrackerMVC.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceDescriptors)
        {
            //Services
            serviceDescriptors.AddTransient<IDeliveryService, DeliveryService>();
            serviceDescriptors.AddTransient<IReceiverService, ReceiverService>();
            serviceDescriptors.AddTransient<ISenderService, SenderService>();


            //AutoMapper
            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());

            //FluentValidator 
            serviceDescriptors.AddScoped<IValidator<NewReceiverVm>, NewReceiverValidator>();
            serviceDescriptors.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }
    }
}
