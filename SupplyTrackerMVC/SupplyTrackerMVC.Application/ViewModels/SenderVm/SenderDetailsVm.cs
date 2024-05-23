using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.SenderVm
{
    public class SenderDetailsVm : IMapFrom<Sender>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressVm Address { get; set; }
        public ContactVm Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SenderDetailsVm, Sender>();
            profile.CreateMap<AddressVm, Address>();
            profile.CreateMap<ContactVm, Contact>();
        }
    }
}
