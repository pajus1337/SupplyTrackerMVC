using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Receivers;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class NewReceiverVm : IMapFrom<Receiver>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NewAddressForReceiverVm Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewReceiverVm, Receiver>()
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address));

            profile.CreateMap<NewAddressForReceiverVm, Address>();
        }
        public class NewReceiverValidator : AbstractValidator<NewReceiverVm>
        {
            public NewReceiverValidator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.Name).Length(0, 5);
                RuleFor(x => x.Address).SetValidator(new NewAddressForReceiverVmValidator());
            }
        }
    }
}
