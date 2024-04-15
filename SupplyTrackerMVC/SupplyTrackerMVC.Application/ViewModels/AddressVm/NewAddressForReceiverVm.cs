using FluentValidation;
using SupplyTrackerMVC.Domain.Model.Addresses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.AddressVm
{
    public class NewAddressForReceiverVm
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
    }

    public class NewAddressForReceiverVmValidator : AbstractValidator<NewAddressForReceiverVm>
    {
        public NewAddressForReceiverVmValidator()
        {
            RuleFor(p => p.Street).NotNull().MinimumLength(3);
            RuleFor(p => p.City).NotNull().MinimumLength(3);
            RuleFor(p => p.ZIP).NotNull().MinimumLength(3);
            RuleFor(p => p.Country).NotNull().MinimumLength(3);
        }
    }
}
