using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Factories;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using System.Reflection;
using static SupplyTrackerMVC.Application.ViewModels.ProductVm.NewProductTypeVm;
using static SupplyTrackerMVC.Application.ViewModels.ProductVm.NewProductVm;
using static SupplyTrackerMVC.Application.ViewModels.ProductVm.UpdateProductVm;
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
            serviceDescriptors.AddTransient<IProductService, ProductService>();
            serviceDescriptors.AddTransient<ISenderService, SenderService>();
            serviceDescriptors.AddTransient<IContactService, ContactService>();

            //AutoMapper
            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());

            //FluentValidator 
            serviceDescriptors.AddTransient<IFluentValidatorFactory, FluentValidatorFactory>();
            serviceDescriptors.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);

            return serviceDescriptors;
        }
    }
}
