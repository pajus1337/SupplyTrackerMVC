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
    public class AddContactDetailVm : IMapFrom<ContactDetail>
    {
        public int Id { get; set; }
        public int ContactDetailTypeId { get; set; }
        [DisplayName("Select communication type")]
        public ContactDetailTypeSelectListVm ContactDetailTypeSelectList { get; set; }
        [DisplayName("Entry Value for new communication type")]
        public string ContactDetailValue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddContactDetailVm, ContactDetail>();
        }
    }

    public class NewContactDetailValidator : AbstractValidator<AddContactDetailVm>
    {
        public NewContactDetailValidator()
        {
            RuleFor(x => x.ContactDetailTypeId).NotNull();
            RuleFor(x => x.ContactDetailValue).NotNull().Length(2, 20);
        }
    }
}
