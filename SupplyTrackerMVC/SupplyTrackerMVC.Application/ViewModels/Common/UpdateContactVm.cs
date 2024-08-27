using AutoMapper;
using FluentValidation;
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
    public class UpdateContactVm : IMapFrom<Contact>
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Role { get; set; }
        public UpdateContactDetailVm ContactDetailVm { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateContactVm, Contact>();
        }

        public class UpdateContactValidator : AbstractValidator<UpdateContactVm>
        {
            public UpdateContactValidator()
            {
                RuleFor(x => x.Id).NotNull().GreaterThan(0);
                RuleFor(x => x.FirstName).NotNull().Length(2, 20);
                RuleFor(x => x.LastName).NotNull().Length(2, 20);
                RuleFor(x => x.Role).NotNull().Length(2, 20);
            }
        }
    }
}
