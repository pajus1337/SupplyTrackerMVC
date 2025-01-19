using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class ContactPersonVm : IMapFrom<Contact>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string CompanyName { get; set; }
        public ICollection<ContactDetailVm> ContactDetails { get; set; }

        public void Mappig(Profile profile)
        {
            profile.CreateMap<Contact, ContactPersonVm>()
                .ForMember(d => d.ContactDetails, opt => opt.MapFrom(s => s.ContactDetails))
                .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Sender.Name ?? s.Receiver.Name) );
        }
    }
}
