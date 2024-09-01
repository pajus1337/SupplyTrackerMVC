using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Addresses;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class UpdateAddressDetailsVm : IMapFrom<Address>
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressDetailsVm>().ReverseMap();
        }

        public class UpdateAddressDetailsValidator : AbstractValidator<UpdateAddressDetailsVm>
        {
            public UpdateAddressDetailsValidator()
            {
                RuleFor(x => x.Street).NotNull().Length(3, 20);
                RuleFor(x => x.City).NotNull().Length(3, 20);
                RuleFor(x => x.ZIP).NotNull().Length(3, 20);
            }
        }
    }
}
