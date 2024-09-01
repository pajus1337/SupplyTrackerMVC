using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class AddAddressVm : IMapFrom<Address>
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }

        public void Mappng(Profile profile)
        {
            profile.CreateMap<AddAddressVm, Address>();
        }

        public class NewAddressValidator : AbstractValidator<AddAddressVm>
        {
            public NewAddressValidator()
            {
                RuleFor(x => x.Street).NotNull().Length(3, 20);
                RuleFor(x => x.City).NotNull().Length(3, 20);
                RuleFor(x => x.ZIP).NotNull().Length(3, 20);
            }
        }
    }
}
