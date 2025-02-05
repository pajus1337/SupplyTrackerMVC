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
        public int ContactId { get; set; }
        public int ContactDetailTypeId { get; set; }
        [DisplayName("Select communication type")]
        public List<ContactDetailTypeForListVm> ContactDetailTypeSelectList { get; set; }
        [DisplayName("Entry Value for new communication type")]
        public string ContactDetailValue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddContactDetailVm, ContactDetail>()
                .ForMember(d => d.ContactDetailTypeId, opt => opt.MapFrom(s => s.ContactDetailTypeId));

        }
    }

    public class AddContactDetailValidator : AbstractValidator<AddContactDetailVm>
    {
        public AddContactDetailValidator()
        {
            RuleFor(x => x.ContactDetailTypeId).NotNull();
            RuleFor(x => x.ContactDetailValue).NotNull().Length(2, 20);
        }
    }
}
