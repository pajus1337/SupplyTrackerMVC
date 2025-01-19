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
    public class ContactVm : IMapFrom<Contact>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public ICollection<ContactDetailVm> ContactDetails { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactVm>()
                .ForMember(d => d.ContactDetails, opt => opt.MapFrom(s => s.ContactDetails));
        }
    }
}
