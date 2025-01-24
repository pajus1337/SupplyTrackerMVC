using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Factories
{
    public class FluentValidatorFactory : IFluentValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IValidator<T> GetValidator<T>() where T : class => _serviceProvider.GetRequiredService<IValidator<T>>();
    }
}
