using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Senders;
using static SupplyTrackerMVC.Application.ViewModels.SenderVm.NewContactForSenderVm;

namespace SupplyTrackerMVC.Application.ViewModels.SenderVm
{
    public class NewSenderVm : IMapFrom<Sender>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NewAddressForSenderVm Address { get; set; }
        public NewContactForSenderVm Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewSenderVm, Sender>()
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address));

            profile.CreateMap<NewAddressForSenderVm, Address>();
            profile.CreateMap<NewContactForSenderVm, Contact>();
        }
        public class NewSenderValidator : AbstractValidator<NewSenderVm>
        {
            public NewSenderValidator()
            {
                RuleFor(x => x.Name).Must(x => !string.IsNullOrWhiteSpace(x)).Length(2, 19);
                RuleFor(x => x.Address).SetValidator(new NewAddressForSenderVmValidator());
                RuleFor(x => x.Contacts).SetValidator(new NewContactForSenderVmValidator());
            }
        }
    }
}
