
using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Receivers;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverDetailsVm : IMapFrom<Receiver>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZIP { get; set; }
        public AddressVm Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Receiver, ReceiverDetailsVm>();
            profile.CreateMap<Address, AddressVm>();
        }
    }
}
