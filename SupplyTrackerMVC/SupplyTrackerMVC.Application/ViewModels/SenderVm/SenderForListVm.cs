using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.AddressVm;
using SupplyTrackerMVC.Application.ViewModels.ContactVm;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.SenderVm
{
    public class SenderForListVm : IMapFrom<Sender>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ContactForListVm> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sender, SenderForListVm>()
                .ForMember(d => d.Contacts, opt => opt.MapFrom(s => s.Contacts));
            profile.CreateMap<Contact, ContactForListVm>();
        }
    }
}
