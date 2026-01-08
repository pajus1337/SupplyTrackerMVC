using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Factories;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Services;
using System.Reflection;

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
            serviceDescriptors.AddTransient<IReportService, ReportService>();
            serviceDescriptors.AddTransient<IStatisticsService, StatisticsService>();
            serviceDescriptors.AddTransient<IUserService, UserService>();

            //AutoMapper
            serviceDescriptors.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            //FluentValidator 
            serviceDescriptors.AddTransient<IFluentValidatorFactory, FluentValidatorFactory>();
            serviceDescriptors.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);

            return serviceDescriptors;
        }
    }
}
