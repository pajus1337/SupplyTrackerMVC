using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class NewContactVm : IMapFrom<Contact>
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Role { get; set; }

        public NewContactDetailVm ContactDetailVm { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewContactVm, Contact>();
        }
    }
}
