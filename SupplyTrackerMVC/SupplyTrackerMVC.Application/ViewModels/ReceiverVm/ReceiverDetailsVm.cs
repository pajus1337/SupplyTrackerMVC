using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Receivers;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverDetailsVm : IMapFrom<Receiver>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressVm Address { get; set; }
        public ICollection<ContactVm> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Receiver, ReceiverDetailsVm>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
                .ReverseMap();

            profile.CreateMap<Contact, ContactVm>().ReverseMap();
            profile.CreateMap<Address, AddressVm>().ReverseMap();
        }

    }
}
