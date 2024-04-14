using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.AddressVm;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
