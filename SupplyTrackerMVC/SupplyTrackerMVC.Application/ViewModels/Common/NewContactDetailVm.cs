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
    public class NewContactDetailVm : IMapFrom<ContactDetail>
    {
        public int Id { get; set; }
        public int SelectedContactDetailTypeId { get; set; }
        public ContactDetailTypeSelectListVm ContactDetailTypeSelectList { get; set; }
        public string ContactDetailValue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewContactDetailVm, ContactDetail>();
        }
    }
}
