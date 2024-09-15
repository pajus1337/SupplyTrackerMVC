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
    public class ContactDetailVm : IMapFrom<ContactDetail>
    {
        public string ContactDetailTypeName { get; set; }
        public string ContactDetailValue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDetail, ContactDetailVm>()
                .ForMember(dest => dest.ContactDetailTypeName, opt => opt.MapFrom(src => src.ContactDetailType.Name));
        }
    }
}
