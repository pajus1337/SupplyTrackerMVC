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
    public class ContactDetailsVm : IMapFrom<Contact>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string ContactDetailTypeName { get; set; }
        public string ContactDetailValue { get; set; }



        public void Mappig(Profile profile)
        {
            profile.CreateMap<Contact, ContactDetailsVm>();
            profile.CreateMap<ContactDetail, ContactDetailsVm>()
                .ForMember(d => d.ContactDetailTypeName, opt => opt.MapFrom(s => s.ContactDetailType.Name));
        }
    }
}
